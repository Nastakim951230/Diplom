using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using _41PP_TRifonova;
using System.Windows.Controls;
using System.Reflection;
using System.Text.RegularExpressions;

namespace UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        

        [TestMethod]
        //Проверяет правильность приобразования номера телефона если есть +7
        public void PhoneNumberVerificationStartingWith7()
        {
      
            string telefon = "+79045126332";
            string otv = "+7(904)512-63-32";
            string actual = PageAddAndUpdateEmployees.Telefon(telefon);
            Assert.AreEqual(otv, actual);
        }

        //Проверяет правильность приобразования номера телефона если нент +7, а есть 8
        [TestMethod]
        public void PhoneNumberVerificationStartingWith8()
        {

            string telefon = "89045126332";
            string otv = "+7(904)512-63-32";
            string actual = PageAddAndUpdateEmployees.Telefon(telefon);
            Assert.AreEqual(otv, actual);
        }


        //Проверяет на ошибку если не правильно введен номер телефона (размер номера)
        [TestMethod]
        public void CheckForAnErrorIfThePhoneNumberIsNotEnteredCorrectly()
        {

            string telefon = "9045126332";
            string otv = "1";
            string actual = PageAddAndUpdateEmployees.Telefon(telefon);
            Assert.AreEqual(otv, actual);
        }

        //Проверяет тип сохранения номера 
        [TestMethod]
        public void TypeOfNumber()
        {

            string telefon = "89045126332";
            string otv = "+7(904)512-63-32";
            string actual = PageAddAndUpdateEmployees.Telefon(telefon);
            Assert.IsInstanceOfType(actual, typeof(string));
        }

        //Проверяет правильность приобразования номера телефона
        [TestMethod]
        public void PhoneNumberVerification()
        {

            string telefon = "89045126332";
            bool otv = true;
            bool actual = PageAddAndUpdateEmployees.numberTelefon(telefon);
            Assert.AreEqual(otv, actual);
        }
    }
}
