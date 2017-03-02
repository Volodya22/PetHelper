using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using TestRest.Services;

namespace PetHelperTests
{
    [TestFixture]
    public class MailTests
    {
        [Test]
        public void MailSendTest()
        {
            var mail = new MailService();

            mail.Send("cheboxarov22@gmail.com", "Test message", "Test");
        }
    }
}
