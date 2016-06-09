using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text.RegularExpressions;

namespace StringCalculator
{
	[TestClass]
	public class UnitTest1
	{
			Calculator calculator;

		[TestInitialize]
		public void Init()
		{
			calculator = new Calculator();
		}

		private void sum(string value,int equalTo)
		{
			var result = calculator.Add(value);
			Assert.AreEqual(equalTo, result);
		}

		[TestMethod]
		public void TestEmptyString()
		{
			sum("",0);
		}

		[TestMethod]
		public void TestOneNumberString()
		{
			sum("10000", 10000);
		}

		[TestMethod]
		public void TestTwoNumberString()
		{
			sum("10000,200", 10200);
		}

		[TestMethod]
		[ExpectedException(typeof (NullReferenceException))]
		public void TestNullString()
		{
			calculator.Add(null);
		}

		[TestMethod]
		[ExpectedException(typeof(FormatException))]
		public void TestAnAlphabeticString()
		{
			calculator.Add("ciao");
		}

		[TestMethod]
		[ExpectedException(typeof(FormatException))]
		public void TestAlmostAnAlphabeticString()
		{
			calculator.Add("ciao,0");
		}

		[TestMethod]
		public void TestNewLinesInString()
		{
			sum("1\n2,3", 6);
		}
		[TestMethod]
		[ExpectedException(typeof(FormatException))]
		public void TestWrongNewLinesInString()
		{
			calculator.Add("1,\n,4");
		}

		//[TestMethod]
		//public void TestDelimiterInString()
		//{
		//	sum("//;\n1;2", 3);
		//}
	}

	public class Calculator
	{
		public int Add(string value) {

			if (value == null)
				throw new NullReferenceException();

			if (value.Length == 0)
				return 0;

			
			char[] delimiter = new char[] {',','\n' };
			if (value.Contains(",")|| value.Contains("\n"))
			{
				var values = value.Split(delimiter);
				var sum = 0;
				for (int i = 0; i< values.Length; i++)
				{
					sum = sum + Int32.Parse(values[i]);
				}
				return sum;
			}

			return Int32.Parse(value);
		}
	}
}
