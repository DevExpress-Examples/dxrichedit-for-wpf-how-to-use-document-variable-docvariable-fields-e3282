' Developer Express Code Central Example:
' How to use document variable (DOCVARIABLE) fields
' 
' This example illustrates the use of a DOCVARIABLE field to provide additional
' information which is dependent on the value of a merged field. This technique is
' implemented so each merged document contains geocoordinates and a weather report
' for a location that corresponds to the current data record.
' Coordinates and
' weather conditions are provided by Google.
' Google services are queried using
' the string representing location. It contains the value of a corresponding
' merged field for each merged document.
' The location is represented by a merge
' field. It is included as an argument within the DOCVARIABLE field. When the
' DOCVARIABLE field is updated, the
' DevExpress.XtraRichEdit.API.Native.Document.CalculateDocumentVariable event is
' triggered. A code within the event handler obtains the information on
' geocoordinates and weather. It uses e.VariableName to get the name of the
' variable within the field, e.Arguments to get the location and returns the
' calculated result in e.Value property.
' The MailMergeRecordStarted event is
' handled to insert a hidden text indicating when the document is created.
' The
' MyProgressIndicatorService class is implemented and registered as a service to
' allow progress indication using the ProgressBar control.
' 
' You can find sample updates and versions for different programming languages here:
' http://www.devexpress.com/example=E3099


Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Net
Imports System.Web
Imports System.IO
Imports System.Text.RegularExpressions
Imports System.Xml
Imports System.Globalization
Imports DevExpress.Utils

Namespace DocumentVariablesExample
	Public Class GeoLocation
		Private _Latitude As Double
		Public Property Latitude() As Double
			Get
				Return _Latitude
			End Get
			Set(ByVal value As Double)
				_Latitude = value
			End Set
		End Property
		Private _Longitude As Double
		Public Property Longitude() As Double
			Get
				Return _Longitude
			End Get
			Set(ByVal value As Double)
				_Longitude = value
			End Set
		End Property
		Private _Address As String
		Public Property Address() As String
			Get
				Return _Address
			End Get
			Set(ByVal value As String)
				_Address = value
			End Set
		End Property


		Public Shared Function GeocodeAddress(ByVal address As String) As GeoLocation()
			Dim coordinates As New List(Of GeoLocation)()
			Dim xmlDoc As New XmlDocument()

			Try
				Dim wbc As New WebClient()
				Dim bytes() As Byte = wbc.DownloadData(String.Format("http://maps.googleapis.com/maps/api/geocode/xml?address={0}&sensor=false", HttpUtility.UrlEncode(address)))
				Dim detector As New EncodingDetector()
				Dim encoding As Encoding = detector.Detect(bytes)
				If encoding Is Nothing Then
					encoding = Encoding.UTF8
				End If
				Dim response As String = encoding.GetString(bytes)
				xmlDoc.LoadXml(response)
			Catch ex As Exception
				If ex.Message IsNot Nothing Then
					Return coordinates.ToArray()
				End If
			End Try

			Dim status As String = xmlDoc.DocumentElement.SelectSingleNode("status").InnerText
			Select Case status.ToLowerInvariant()
			Case "ok"
				' Everything went just fine
			Case "zero_results"
				Return coordinates.ToArray()
			Case "over_query_limit", "invalid_request", "request_denied"
				Throw New Exception("An error occured when contacting the Google Maps API.") ' Should probably be refined to something more useful like throwing specific exceptions for each error
			End Select

			Dim nodeCol As XmlNodeList = xmlDoc.DocumentElement.SelectNodes("result")
			For Each node As XmlNode In nodeCol
				Dim exact_address As String = node.SelectSingleNode("formatted_address").InnerText
				Dim lat As Double = Convert.ToDouble(node.SelectSingleNode("geometry/location/lat").InnerText, CultureInfo.InvariantCulture)
				Dim lng As Double = Convert.ToDouble(node.SelectSingleNode("geometry/location/lng").InnerText, CultureInfo.InvariantCulture)

				Dim coord As New GeoLocation()
				coord.Address = exact_address
				coord.Latitude = lat
				coord.Longitude = lng
				coordinates.Add(coord)
			Next node
			Return coordinates.ToArray()
		End Function
	End Class
End Namespace
