Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Text
Imports DevExpress.XtraEditors
Imports DevExpress.Services
Imports System.Windows.Forms
Imports DevExpress.Xpf.Editors
Imports System.Windows.Threading

Namespace DocumentVariablesExample
	#Region "#myprogressindicator"
	Friend Class MyProgressIndicatorService
		Implements IProgressIndicationService
		Private _Indicator As ProgressBarEdit
		Public Property Indicator() As ProgressBarEdit
			Get
				Return _Indicator
			End Get
			Set(ByVal value As ProgressBarEdit)
				_Indicator = value
			End Set
		End Property

		Public Sub New(ByVal provider As IServiceProvider, ByVal indicator As ProgressBarEdit)
			_Indicator = indicator
		End Sub

		#Region "IProgressIndicationService Members"

		Private Sub Begin(ByVal displayName As String, ByVal minProgress As Integer, ByVal maxProgress As Integer, ByVal currentProgress As Integer) Implements IProgressIndicationService.Begin
			_Indicator.Minimum = minProgress
			_Indicator.Maximum = maxProgress
			_Indicator.EditValue = currentProgress
			_Indicator.Visibility = System.Windows.Visibility.Visible
			Refresh()
		End Sub

		Private Sub [End]() Implements IProgressIndicationService.End
			_Indicator.Visibility = System.Windows.Visibility.Collapsed
			Refresh()
		End Sub

		Private Sub SetProgress(ByVal currentProgress As Integer) Implements IProgressIndicationService.SetProgress
			_Indicator.EditValue = currentProgress
			Refresh()
		End Sub
		#End Region

		Private Sub Refresh()
			Dim emptyDelegate As Action = Function() AnonymousMethod1()
			_Indicator.Dispatcher.Invoke(DispatcherPriority.Render, emptyDelegate)
		End Sub
		
		Private Function AnonymousMethod1() As Boolean
			Return True
		End Function
	End Class
#End Region ' #myprogressindicatorsindicator
End Namespace
