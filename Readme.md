<!-- default file list -->
*Files to look at*:

* [GeoLocation.cs](./CS/GeoLocation.cs) (VB: [GeoLocation.vb](./VB/GeoLocation.vb))
* [MainWindow.xaml](./CS/MainWindow.xaml) (VB: [MainWindow.xaml](./VB/MainWindow.xaml))
* [MainWindow.xaml.cs](./CS/MainWindow.xaml.cs) (VB: [MainWindow.xaml.vb](./VB/MainWindow.xaml.vb))
* [MyProgressIndicator.cs](./CS/MyProgressIndicator.cs) (VB: [MyProgressIndicator.vb](./VB/MyProgressIndicator.vb))
<!-- default file list end -->
# DXRichEdit for WPF: How to use document variable (DOCVARIABLE) fields


This example illustrates the use of a [DOCVARIABLE](https://docs.devexpress.com/WPF/10299/Controls-and-Libraries/Rich-Text-Editor/Fields/Field-Codes/DOCVARIABLE) field to provide additional information which is dependent on the value of a merged field. This technique is implemented so each merged document contains geocoordinates for a location that corresponds to the current data record.
NB: We do not provide code for retrieving geocoding and weather information, empty data are returned instead. You can implement a custom geocoordinate and weather information provider.
The location is represented by a merge field. It is included as an argument within the DOCVARIABLE field. When the DOCVARIABLE field is updated, the [Document.CalculateDocumentVariable](https://docs.devexpress.com/WindowsForms/DevExpress.XtraRichEdit.RichEditControl.CalculateDocumentVariable) event is triggered. A code within the event handler obtains the information on geocoordinates. It uses *e.VariableName* to get the name of the variable within the field, *e.Arguments* to get the location and returns the calculated result in *e.Value* property.
The [MailMergeRecordStarted](https://docs.devexpress.com/WPF/DevExpress.Xpf.RichEdit.RichEditControl.MailMergeRecordStarted) event is handled to insert a hidden text indicating when the document is created. To display hidden text and all non-printing characters, use the CTRL-SHIFT-8 key combination.
The <strong>MyProgressIndicatorService</strong> class is implemented and registered as a service to allow progress indication using the ProgressBar control.





