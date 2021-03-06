@echo This file contains commands to execute tests in suites with NUnit Console Runner
@echo and then to produce tests reports with NUnit Console Runner as well as SpecFlow.

@echo The file paths here are relative and are adjusted for these commands to be run as a Custom script Build step in Team City
@echo or in Command prompt directly in the Agent working directory, i.e. the checkout directory /work/{hash value working dir}


@echo Run tests with priority = 1 - Regression / Smoke

packages\NUnit.ConsoleRunner.3.6.1\tools\nunit3-console.exe --labels=All --out=Blog.UI.Tests\TestResult-Prio=1.txt --result=Blog.UI.Tests\TestResult-Prio=1.xml;format=nunit2 Blog.UI.Tests\bin\Debug\Blog.UI.Tests.dll --where "Category == Regression"
packages\ReportUnit.1.5.0-beta1\tools\reportunit.exe Blog.UI.Tests\TestResult-Prio=1.xml Blog.UI.Tests\TestResult-Prio=1.html
packages\SpecFlow.2.1.0\tools\specflow.exe nunitexecutionreport Blog.UI.Tests\Blog.UI.Tests.csproj /xmlTestResult:"Blog.UI.Tests\TestResult-Prio=1.xml" /out:"Blog.UI.Tests\TestResult-Prio=1.htm"


@echo Run tests with priority = 2 - Navigation

packages\NUnit.ConsoleRunner.3.6.1\tools\nunit3-console.exe --labels=All --out=Blog.UI.Tests\TestResult-Prio=2.txt --result=Blog.UI.Tests\TestResult-Prio=2.xml;format=nunit2 Blog.UI.Tests\bin\Debug\Blog.UI.Tests.dll --where "Category == Navigation"
packages\ReportUnit.1.5.0-beta1\tools\reportunit.exe Blog.UI.Tests\TestResult-Prio=2.xml Blog.UI.Tests\TestResult-Prio=2.html
packages\SpecFlow.2.1.0\tools\specflow.exe nunitexecutionreport Blog.UI.Tests\Blog.UI.Tests.csproj /xmlTestResult:"Blog.UI.Tests\TestResult-Prio=2.xml" /out:"Blog.UI.Tests\TestResult-Prio=2.htm"


@echo Run tests with priority = 3 - Remaining tests - mainly Negative cases

packages\NUnit.ConsoleRunner.3.6.1\tools\nunit3-console.exe --labels=All --out=Blog.UI.Tests\TestResult-Prio=3.txt --result=Blog.UI.Tests\TestResult-Prio=3.xml;format=nunit2 Blog.UI.Tests\bin\Debug\Blog.UI.Tests.dll --where "Category == Negative"
packages\ReportUnit.1.5.0-beta1\tools\reportunit.exe Blog.UI.Tests\TestResult-Prio=3.xml Blog.UI.Tests\TestResult-Prio=3.html
packages\SpecFlow.2.1.0\tools\specflow.exe nunitexecutionreport Blog.UI.Tests\Blog.UI.Tests.csproj /xmlTestResult:"Blog.UI.Tests\TestResult-Prio=3.xml" /out:"Blog.UI.Tests\TestResult-Prio=3.htm"


@echo for running all the tests and creating ReportUnit and SpecFlow test reports:

packages\NUnit.ConsoleRunner.3.6.1\tools\nunit3-console.exe --labels=All --out=Blog.UI.Tests\TestResult.txt --result=Blog.UI.Tests\TestResult.xml;format=nunit2 Blog.UI.Tests\bin\Debug\Blog.UI.Tests.dll
packages\ReportUnit.1.5.0-beta1\tools\reportunit.exe Blog.UI.Tests\TestResult.xml Blog.UI.Tests\TestResult.html
packages\SpecFlow.2.1.0\tools\specflow.exe nunitexecutionreport Blog.UI.Tests\Blog.UI.Tests.csproj /xmlTestResult:"Blog.UI.Tests\TestResult.xml" /out:"Blog.UI.Tests\TestResult.htm"
