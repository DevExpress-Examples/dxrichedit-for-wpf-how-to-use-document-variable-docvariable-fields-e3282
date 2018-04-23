Imports Microsoft.VisualBasic
Imports System
Imports System.Windows
Imports DevExpress.XtraRichEdit.API.Native
Imports DevExpress.XtraRichEdit
Imports DevExpress.Xpf.RichEdit

#Region "#usings"
Imports DevExpress.Services
Imports System.Drawing
#End Region ' #usings

Namespace DocumentVariablesExample
	''' <summary>
	''' Interaction logic for MainWindow.xaml
	''' </summary>
	Partial Public Class MainWindow
		Inherits Window
		Private richEdit As RichEditControl

		Public Sub New()
			InitializeComponent()
		End Sub


		Private Sub richEditControl1_Loaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
			richEditControl1.ApplyTemplate()
			richEditControl1.LoadDocument("Docs\invitation.docx")
			richEditControl1.Options.MailMerge.DataSource = New SampleData()
			AddHandler richEditControl2.Document.CalculateDocumentVariable, AddressOf eventHandler_CalculateDocumentVariable
			Me.richEdit = richEditControl1
		End Sub

		Private Sub btnMailMerge_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
			Dim myMergeOptions As MailMergeOptions = richEditControl1.Document.CreateMailMergeOptions()
			myMergeOptions.MergeMode = MergeMode.NewSection
			richEditControl1.Document.MailMerge(myMergeOptions, richEditControl2.Document)
			tabControl.SelectedIndex = 1
		End Sub

		#Region "#servicesubst"
		Private Sub richEditControl1_MailMergeStarted(ByVal sender As Object, ByVal e As MailMergeStartedEventArgs)
			richEditControl1.RemoveService(GetType(IProgressIndicationService))
			richEditControl1.AddService(GetType(IProgressIndicationService), New MyProgressIndicatorService(richEditControl1, Me.progressBarControl1))
		End Sub
		#End Region ' #servicesubst

		Private Sub richEditControl1_MailMergeFinished(ByVal sender As Object, ByVal e As MailMergeFinishedEventArgs)
			richEditControl1.RemoveService(GetType(IProgressIndicationService))
		End Sub

		#Region "#mailmergerecordstarted"
		Private Sub richEditControl1_MailMergeRecordStarted(ByVal sender As Object, ByVal e As MailMergeRecordStartedEventArgs)
			Dim _range As DocumentRange = e.RecordDocument.InsertText(e.RecordDocument.Range.Start, String.Format("Created on {0:G}" & Constants.vbLf + Constants.vbLf, DateTime.Now))
			Dim cp As CharacterProperties = e.RecordDocument.BeginUpdateCharacters(_range)
			cp.FontSize = 8
			cp.ForeColor = Color.Red
			cp.Hidden = True
			e.RecordDocument.EndUpdateCharacters(cp)
		End Sub
		#End Region ' #mailmergerecordstarted

		#Region "#mailmergerecordfinished"
		Private Sub richEditControl1_MailMergeRecordFinished(ByVal sender As Object, ByVal e As MailMergeRecordFinishedEventArgs)
			e.RecordDocument.AppendDocumentContent("Docs\bungalow.docx", DocumentFormat.OpenXml)
		End Sub
		#End Region ' #mailmergerecordfinished

		#Region "#calculatedocumentvariable"
		Private Sub eventHandler_CalculateDocumentVariable(ByVal sender As Object, ByVal e As CalculateDocumentVariableEventArgs)
			Dim location As String = e.Arguments(0).Value.ToString()

			Console.WriteLine(e.VariableName & " " & location)

			If (location.Trim() = String.Empty) OrElse (location.Contains("<")) Then
				e.Value = " "
				e.Handled = True
				Return
			End If

			Select Case e.VariableName
				Case "Weather"
					Dim conditions As New Conditions()
					conditions = Weather.GetCurrentConditions(location)
					e.Value = String.Format("Forecast for {0}: " & Constants.vbLf & "Conditions: {1}" & Constants.vbLf & "Temperature (C) :{2}" & Constants.vbLf & "Humidity: {3}" & Constants.vbLf & "Wind: {4}" & Constants.vbLf, conditions.City, conditions.Condition, conditions.TempC, conditions.Humidity, conditions.Wind)
				Case "Location"
					Dim loc() As GeoLocation = GeoLocation.GeocodeAddress(location)
					e.Value = String.Format(" {0}" & Constants.vbLf & "Latitude: {1}" & Constants.vbLf & "Longitude: {2}" & Constants.vbLf, loc(0).Address, loc(0).Latitude.ToString(), loc(0).Longitude.ToString())
			End Select
			e.Handled = True
		End Sub
		#End Region ' #calculatedocumentvariable

		Private Sub tabControl_SelectionChanged(ByVal sender As Object, ByVal e As DevExpress.Xpf.Core.TabControlSelectionChangedEventArgs)
			Select Case tabControl.SelectedIndex
				Case 0
					richEdit = richEditControl1
					Me.btnMailMerge.IsEnabled = True
				Case 1
					richEdit = richEditControl2
					Me.btnMailMerge.IsEnabled = False
			End Select
		End Sub

		Private Sub chkShowCodes_Checked(ByVal sender As Object, ByVal e As RoutedEventArgs)
			ShowFieldCodes(True)
		End Sub

		Private Sub chkShowCodes_Unchecked(ByVal sender As Object, ByVal e As RoutedEventArgs)
			ShowFieldCodes(False)
		End Sub

		Private Sub ShowFieldCodes(ByVal showCodes As Boolean)
			Dim doc As Document = richEdit.Document
			doc.BeginUpdate()
			For Each f As Field In doc.Fields
				f.ShowCodes = showCodes
			Next f
			doc.EndUpdate()
		End Sub
	End Class
End Namespace
