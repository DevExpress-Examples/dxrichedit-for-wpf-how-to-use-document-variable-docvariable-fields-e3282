Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Xml
Imports System.Net
Imports System.Web
Imports DevExpress.Utils

Namespace DocumentVariablesExample
	Friend Class Weather
		''' <summary>
		''' The function that returns the current conditions for the specified location.
		''' </summary>
		''' <param name="location">City or ZIP code</param>
		''' <returns></returns>
		Public Shared Function GetCurrentConditions(ByVal location As String) As Conditions
			Dim conditions As New Conditions()
			Dim xmlConditions As New XmlDocument()
			Try
				Dim wbc As New WebClient()
				Dim bytes() As Byte = wbc.DownloadData(String.Format("http://www.google.com/ig/api?weather={0}", HttpUtility.UrlEncode(location)))
				Dim detector As New EncodingDetector()
				Dim encoding As Encoding = detector.Detect(bytes)
				If encoding Is Nothing Then
					encoding = Encoding.UTF8
				End If
				Dim response As String = encoding.GetString(bytes)
				xmlConditions.LoadXml(response)
			Catch ex As Exception
				If ex.Message IsNot Nothing Then
				Return conditions
				End If
			End Try

			If xmlConditions.SelectSingleNode("xml_api_reply/weather/problem_cause") Is Nothing Then
				conditions.City = xmlConditions.SelectSingleNode("/xml_api_reply/weather/forecast_information/city").Attributes("data").InnerText
				conditions.Condition = xmlConditions.SelectSingleNode("/xml_api_reply/weather/current_conditions/condition").Attributes("data").InnerText
				conditions.TempC = xmlConditions.SelectSingleNode("/xml_api_reply/weather/current_conditions/temp_c").Attributes("data").InnerText
				conditions.TempF = xmlConditions.SelectSingleNode("/xml_api_reply/weather/current_conditions/temp_f").Attributes("data").InnerText
				conditions.Humidity = xmlConditions.SelectSingleNode("/xml_api_reply/weather/current_conditions/humidity").Attributes("data").InnerText
				conditions.Wind = xmlConditions.SelectSingleNode("/xml_api_reply/weather/current_conditions/wind_condition").Attributes("data").InnerText
			End If

			Return conditions
		End Function

		''' <summary>
		''' The function that gets the forecast for the next four days.
		''' </summary>
		''' <param name="location">City or ZIP code</param>
		''' <returns></returns>
		Public Shared Function GetForecast(ByVal location As String) As List(Of Conditions)
			Dim conditions As New List(Of Conditions)()

			Dim xmlConditions As New XmlDocument()

			Try
				Dim wbc As New WebClient()
				Dim bytes() As Byte = wbc.DownloadData(String.Format("http://www.google.com/ig/api?weather={0}", HttpUtility.UrlEncode(location)))
				Dim detector As New EncodingDetector()
				Dim encoding As Encoding = detector.Detect(bytes)
				If encoding Is Nothing Then
					encoding = Encoding.UTF8
				End If
				Dim response As String = encoding.GetString(bytes)
				xmlConditions.LoadXml(response)
			Catch ex As Exception
				If ex.Message IsNot Nothing Then
					Return conditions
				End If
			End Try

			If xmlConditions.SelectSingleNode("xml_api_reply/weather/problem_cause") IsNot Nothing Then
				conditions = Nothing
			Else
				For Each node As XmlNode In xmlConditions.SelectNodes("/xml_api_reply/weather/forecast_conditions")
					Dim condition As New Conditions()
					condition.City = xmlConditions.SelectSingleNode("/xml_api_reply/weather/forecast_information/city").Attributes("data").InnerText
					condition.Condition = node.SelectSingleNode("condition").Attributes("data").InnerText
					condition.High = node.SelectSingleNode("high").Attributes("data").InnerText
					condition.Low = node.SelectSingleNode("low").Attributes("data").InnerText
					condition.DayOfWeek = node.SelectSingleNode("day_of_week").Attributes("data").InnerText
					conditions.Add(condition)
				Next node
			End If

			Return conditions
		End Function
	End Class
	Public Class Conditions
		Private city_Renamed As String = "No Data"
		Private dayOfWeek_Renamed As String = DateTime.Now.DayOfWeek.ToString()
		Private condition_Renamed As String = "No Data"
		Private tempF_Renamed As String = "No Data"
		Private tempC_Renamed As String = "No Data"
		Private humidity_Renamed As String = "No Data"
		Private wind_Renamed As String = "No Data"
		Private high_Renamed As String = "No Data"
		Private low_Renamed As String = "No Data"

		Public Property City() As String
			Get
				Return city_Renamed
			End Get
			Set(ByVal value As String)
				city_Renamed = value
			End Set
		End Property

		Public Property Condition() As String
			Get
				Return condition_Renamed
			End Get
			Set(ByVal value As String)
				condition_Renamed = value
			End Set
		End Property

		Public Property TempF() As String
			Get
				Return tempF_Renamed
			End Get
			Set(ByVal value As String)
				tempF_Renamed = value
			End Set
		End Property

		Public Property TempC() As String
			Get
				Return tempC_Renamed
			End Get
			Set(ByVal value As String)
				tempC_Renamed = value
			End Set
		End Property

		Public Property Humidity() As String
			Get
				Return humidity_Renamed
			End Get
			Set(ByVal value As String)
				humidity_Renamed = value
			End Set
		End Property

		Public Property Wind() As String
			Get
				Return wind_Renamed
			End Get
			Set(ByVal value As String)
				wind_Renamed = value
			End Set
		End Property

		Public Property DayOfWeek() As String
			Get
				Return dayOfWeek_Renamed
			End Get
			Set(ByVal value As String)
				dayOfWeek_Renamed = value
			End Set
		End Property

		Public Property High() As String
			Get
				Return high_Renamed
			End Get
			Set(ByVal value As String)
				high_Renamed = value
			End Set
		End Property

		Public Property Low() As String
			Get
				Return low_Renamed
			End Get
			Set(ByVal value As String)
				low_Renamed = value
			End Set
		End Property
	End Class
End Namespace
