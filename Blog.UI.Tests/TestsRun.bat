@echo these commands should be run in the Agent working directory, i.e. the checkout directory /work/{hash value working dir}

@echo Run tests with priority = 1 - Regression / Smoke

packages\NUnit.ConsoleRunner.3.6.1\tools\nunit3-console.exe --labels=All --out=Blog.UI.Tests\TestResult-Prio=1.txt --result=Blog.UI.Tests\TestResult-Prio=1.xml;format=nunit2 Blog.UI.Tests\bin\Debug\Blog.UI.Tests.dll --where "Category == Regression"
packages\ReportUnit.1.5.0-beta1\tools\reportunit.exe Blog.UI.Tests\TestResult-Prio=1.xml Blog.UI.Tests\TestResult-Prio=1.html
copy Blog.UI.Tests\TestResult-Prio=1.xml packages\SpecFlow.2.1.0\tools\TestResult-Prio=1.xml
packages\SpecFlow.2.1.0\tools\specflow.exe nunitexecutionreport Blog.UI.Tests\Blog.UI.Tests.csproj /out:Blog.UI.Tests\TestResult-Prio=1.htm


@echo Run tests with priority = 2 - Navigation

packages\NUnit.ConsoleRunner.3.6.1\tools\nunit3-console.exe --labels=All --out=Blog.UI.Tests\TestResult-Prio=2.txt --result=Blog.UI.Tests\TestResult-Prio=2.xml;format=nunit2 Blog.UI.Tests\bin\Debug\Blog.UI.Tests.dll --where "Category == Navigation"
packages\ReportUnit.1.5.0-beta1\tools\reportunit.exe Blog.UI.Tests\TestResult-Prio=2.xml Blog.UI.Tests\TestResult-Prio=2.html
copy Blog.UI.Tests\TestResult-Prio=2.xml packages\SpecFlow.2.1.0\tools\TestResult-Prio=2.xml
packages\SpecFlow.2.1.0\tools\specflow.exe nunitexecutionreport Blog.UI.Tests\Blog.UI.Tests.csproj /out:Blog.UI.Tests\TestResult-Prio=2.htm


@echo Run tests with priority = 3 - Remaining tests - mainly Negative cases

packages\NUnit.ConsoleRunner.3.6.1\tools\nunit3-console.exe --labels=All --out=Blog.UI.Tests\TestResult-Prio=3.txt --result=Blog.UI.Tests\TestResult-Prio=3.xml;format=nunit2 Blog.UI.Tests\bin\Debug\Blog.UI.Tests.dll --where "Category == Negative"
packages\ReportUnit.1.5.0-beta1\tools\reportunit.exe Blog.UI.Tests\TestResult-Prio=3.xml Blog.UI.Tests\TestResult-Prio=3.html
copy Blog.UI.Tests\TestResult-Prio=3.xml packages\SpecFlow.2.1.0\tools\TestResult-Prio=3.xml
packages\SpecFlow.2.1.0\tools\specflow.exe nunitexecutionreport Blog.UI.Tests\Blog.UI.Tests.csproj /out:Blog.UI.Tests\TestResult-Prio=3.htm


@echo for running all the tests and creating ReportUnit and SpecFlow test reports:

packages\NUnit.ConsoleRunner.3.6.1\tools\nunit3-console.exe --labels=All --out=Blog.UI.Tests\TestResult.txt --result=Blog.UI.Tests\TestResult.xml;format=nunit2 Blog.UI.Tests\bin\Debug\Blog.UI.Tests.dll
packages\ReportUnit.1.5.0-beta1\tools\reportunit.exe Blog.UI.Tests\TestResult.xml Blog.UI.Tests\TestResult.html
copy Blog.UI.Tests\TestResult.xml packages\SpecFlow.2.1.0\tools\TestResult.xml
packages\SpecFlow.2.1.0\tools\specflow.exe nunitexecutionreport Blog.UI.Tests\Blog.UI.Tests.csproj /out:Blog.UI.Tests\TestResult.htm