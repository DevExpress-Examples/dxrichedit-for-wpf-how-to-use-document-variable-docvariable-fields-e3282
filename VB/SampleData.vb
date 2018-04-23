Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Collections

Namespace DocumentVariablesExample
	Friend Class SampleData
		Inherits ArrayList
		Public Sub New()
			Add(New AddresseeRecord("Maria", "Alfreds Futterkiste", "Obere Str. 57, Berlin", "Berlin"))
			Add(New AddresseeRecord("Laurence", "Bon app'", "12, rue des Bouchers, Marseille", "Marseille"))
			Add(New AddresseeRecord("Patricio", "Cactus Comidas para llevar", "Cerrito 333, Buenos Aires", "Buenos Aires"))
			Add(New AddresseeRecord("Thomas", "Around the Horn", "120 Hanover Sq., London", "London"))
			Add(New AddresseeRecord("Boris", "Express Developers", "Krasnoarmeiskiy prospect 25, Tula", "Tula"))
			Add(New AddresseeRecord("Christina", "Berglunds snabbkop", "Berguvsvagen  8, Lulea", "Lulea"))
			Add(New AddresseeRecord("Hanna", "Blauer See Delikatessen", "Forsterstr. 57, Mannheim", "Mannheim"))
			Add(New AddresseeRecord("Frederique", "Blondel pere et fils", "24, place Kleber, Strasbourg", "Strasbourg"))
			Add(New AddresseeRecord("Antonio", "Antonio Moreno Taqueria", "Mataderos  2312, Mexico D.F.", "Mexico"))
			Add(New AddresseeRecord("Martin", "Bolido Comidas preparadas", "C/ Araquil, 67, Madrid", "Madrid"))
			Add(New AddresseeRecord("Elizabeth", "Bottom-Dollar Markets", "23 Tsawassen Blvd., Tsawwassen", "Tsawwassen"))
			Add(New AddresseeRecord("Victoria", "B's Beverages", "Fauntleroy Circus, London", "London"))
			Add(New AddresseeRecord("Yang", "Chop-suey Chinese", "Hauptstr. 29, Bern", "Bern"))
			Add(New AddresseeRecord("Pedro", "Comercio Mineiro", "Av. dos Lusiadas, 23, Sao Paulo", "Sao Paulo"))
			Add(New AddresseeRecord("Elizabeth","Consolidated Holdings","Berkeley Gardens12  Brewery , London", "London"))
			Add(New AddresseeRecord("Sven","Drachenblut Delikatessen","Walserweg 21, Aachen", "Aachen"))
			Add(New AddresseeRecord("Janine", "Du monde entier", "67, rue des Cinquante Otages, Nantes", "Nantes"))
			Add(New AddresseeRecord("Zbyszek", "Wolski  Zajazd", "ul. Filtrowa 68, Warszawa", "Warszawa"))
		End Sub
	End Class

	Public Class AddresseeRecord
		Private _Name As String
		Private _Company As String
		Private _Address As String
		Private _City As String

		Public Property Name() As String
			Get
				Return _Name
			End Get
			Set(ByVal value As String)
				_Name = value
			End Set
		End Property
		Public Property Company() As String
			Get
				Return _Company
			End Get
			Set(ByVal value As String)
				_Company = value
			End Set
		End Property
		Public Property Address() As String
			Get
				Return _Address
			End Get
			Set(ByVal value As String)
				_Address = value
			End Set
		End Property
		Public Property City() As String
			Get
				Return _City
			End Get
			Set(ByVal value As String)
				_City = value
			End Set
		End Property

		Public Sub New(ByVal _Name As String, ByVal _Company As String, ByVal _Address As String, ByVal _City As String)
			Me._Name = _Name
			Me._Company = _Company
			Me._Address = _Address
			Me._City = _City
		End Sub
	End Class
End Namespace
