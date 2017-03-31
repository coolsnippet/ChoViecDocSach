we note down this:
    in book: C# Programming Cookbook
        we have IntellisTest (only for Visual Studio Enterprise) that can create and run tests against code contracts
        this is very fast to use!

    mstest attributes:
    [TestClass] = [TestFixture] (Nunit)
    [TestMethod] = [Test] (Nunit)
    [ClassInitialize] = [TestFixtureSetUp] (nunit) Identifies a method which should be called a single time prior to executing any test in the Test Class/Test Fixture
    [TestInitialize] = [SetUp] (nunit): run every time a test method called <> [ClassInitialize] (diff when we test a bunch of classes!)
    