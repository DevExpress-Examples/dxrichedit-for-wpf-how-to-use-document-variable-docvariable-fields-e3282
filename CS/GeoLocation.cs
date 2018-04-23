// Developer Express Code Central Example:
// How to use document variable (DOCVARIABLE) fields
// 
// This example illustrates the use of a DOCVARIABLE field to provide additional
// information which is dependent on the value of a merged field. This technique is
// implemented so each merged document contains geocoordinates and a weather report
// for a location that corresponds to the current data record.
// Coordinates and
// weather conditions are provided by Google.
// Google services are queried using
// the string representing location. It contains the value of a corresponding
// merged field for each merged document.
// The location is represented by a merge
// field. It is included as an argument within the DOCVARIABLE field. When the
// DOCVARIABLE field is updated, the
// DevExpress.XtraRichEdit.API.Native.Document.CalculateDocumentVariable event is
// triggered. A code within the event handler obtains the information on
// geocoordinates and weather. It uses e.VariableName to get the name of the
// variable within the field, e.Arguments to get the location and returns the
// calculated result in e.Value property.
// The MailMergeRecordStarted event is
// handled to insert a hidden text indicating when the document is created.
// The
// MyProgressIndicatorService class is implemented and registered as a service to
// allow progress indication using the ProgressBar control.
// 
// You can find sample updates and versions for different programming languages here:
// http://www.devexpress.com/example=E3099

using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Web;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml;
using System.Globalization;
using DevExpress.Utils;

namespace DocumentVariablesExample
{
    public class GeoLocation
    {
        private double _Latitude;
        public double Latitude
        {
            get { return _Latitude; }
            set
            {
                _Latitude = value;
            }
        }
        private double _Longitude;
        public double Longitude
        {
            get { return _Longitude; }
            set
            {
                _Longitude = value;
            }
        }
        private string _Address;
        public string Address
        {
            get { return _Address; }
            set
            {
                _Address = value;
            }
        }
        

        public static GeoLocation[] GeocodeAddress(string address)
        {
            List<GeoLocation> coordinates = new List<GeoLocation>();
            XmlDocument xmlDoc = new XmlDocument();

            try {
                WebClient wbc = new WebClient();
                byte[] bytes = wbc.DownloadData(string.Format("http://maps.googleapis.com/maps/api/geocode/xml?address={0}&sensor=false", HttpUtility.UrlEncode(address)));
                EncodingDetector detector = new EncodingDetector();
                Encoding encoding = detector.Detect(bytes);
                if (encoding == null)
                    encoding = Encoding.UTF8;
                string response = encoding.GetString(bytes);
                xmlDoc.LoadXml(response);
            }
            catch (Exception ex) {
                if (ex.Message != null)
                    return coordinates.ToArray();
            }
            
            string status = xmlDoc.DocumentElement.SelectSingleNode("status").InnerText;
            switch (status.ToLowerInvariant())
            {
            case "ok":
                // Everything went just fine
                break;
            case "zero_results":
                return coordinates.ToArray();
            case "over_query_limit":
            case "invalid_request":
            case "request_denied":
                throw new Exception("An error occured when contacting the Google Maps API."); // Should probably be refined to something more useful like throwing specific exceptions for each error
            }
            
            XmlNodeList nodeCol = xmlDoc.DocumentElement.SelectNodes("result");
            foreach (XmlNode node in nodeCol)
            {
                string exact_address = node.SelectSingleNode("formatted_address").InnerText;
                double lat = Convert.ToDouble(node.SelectSingleNode("geometry/location/lat").InnerText, CultureInfo.InvariantCulture);
                double lng = Convert.ToDouble(node.SelectSingleNode("geometry/location/lng").InnerText, CultureInfo.InvariantCulture);

                GeoLocation coord = new GeoLocation();
                coord.Address = exact_address;
                coord.Latitude = lat;
                coord.Longitude = lng;
                coordinates.Add(coord);
            }            
            return coordinates.ToArray();
        }
    }
}
