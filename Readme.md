# DXRichEdit for WPF: How to use document variable (DOCVARIABLE) fields


<p>This example illustrates the use of a <strong>DOCVARIABLE </strong>field to provide additional information which is dependent on the value of a merged field. This technique is implemented so each merged document contains geocoordinates for a location that corresponds to the current data record.</p><p>NB: We do not provide code for retrieving geocoding and weather information, empty data are returned instead. You can implement a custom geocoordinate and weather information provider.</p><p>The location is represented by a merge field. It is included as an argument within the DOCVARIABLE field. When the DOCVARIABLE field is updated, the <strong>DevExpress.XtraRichEdit.API.Native.Document.CalculateDocumentVariable</strong> event is triggered. A code within the event handler obtains the information on geocoordinates. It uses <u>e.VariableName</u> to get the name of the variable within the field, <u>e.Arguments</u> to get the location and returns the calculated result in <u>e.Value</u> property.<br />
The <strong>MailMergeRecordStarted</strong> event is handled to insert a hidden text indicating when the document is created. To display hidden text and all non-printing characters, use the CTRL-SHIFT-8 key combination.<br />
The <strong>MyProgressIndicatorService</strong> class is implemented and registered as a service to allow progress indication using the ProgressBar control.</p><br />


<br/>


