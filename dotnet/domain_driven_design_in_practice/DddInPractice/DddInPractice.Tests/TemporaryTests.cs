using DddInPractice.Logic;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace DddInPractice.Tests
{
    public class TemporaryTests
    {
        [Fact]
        public void Test()
        {
            SessionFactory.Init(@"Server=(localdb)\MSSQLLocalDB; Database=DddInPractice; Trusted_Connection=true");

            using (var session = SessionFactory.OpenSession())
            {
                long id = 1L;
                var snackMachine = session.Get<SnackMachine>(id);
            }
        }
    }
}