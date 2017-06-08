@echo This file contains commands to execute tests with NUnit Console Runner
@echo and then to produce tests reports with NUnit Console Runner as well as SpecFlow.


@echo The file paths here are absolute and are adjusted for these commands to be run for TeamCityBlogProject in Command prompt on my computer:

"C:\Users\vaneto\Documents\Visual Studio 2015\Projects\Blog-Skeleton\Blog-Skeleton\packages\NUnit.ConsoleRunner.3.6.1\tools\nunit3-console.exe" --labels=All --out="C:\Users\vaneto\Documents\Visual Studio 2015\Projects\Blog-Skeleton\Blog-Skeleton\Blog.UI.Tests\TestResult.txt" --result="C:\Users\vaneto\Documents\Visual Studio 2015\Projects\Blog-Skeleton\Blog-Skeleton\Blog.UI.Tests\TestResult.xml";format=nunit2 "C:\Users\vaneto\Documents\Visual Studio 2015\Projects\Blog-Skeleton\Blog-Skeleton\Blog.UI.Tests\bin\Debug\Blog.UI.Tests.dll"
"C:\Users\vaneto\Documents\Visual Studio 2015\Projects\Blog-Skeleton\Blog-Skeleton\packages\ReportUnit.1.5.0-beta1\tools\reportunit.exe" "C:\Users\vaneto\Documents\Visual Studio 2015\Projects\Blog-Skeleton\Blog-Skeleton\Blog.UI.Tests\TestResult.xml" "C:\Users\vaneto\Documents\Visual Studio 2015\Projects\Blog-Skeleton\Blog-Skeleton\Blog.UI.Tests\TestResult.html"
copy "C:\Users\vaneto\Documents\Visual Studio 2015\Projects\Blog-Skeleton\Blog-Skeleton\Blog.UI.Tests\TestResult.xml" "TestResult.xml"
"C:\Users\vaneto\Documents\Visual Studio 2015\Projects\Blog-Skeleton\Blog-Skeleton\packages\SpecFlow.2.1.0\tools\specflow.exe" nunitexecutionreport "C:\Users\vaneto\Documents\Visual Studio 2015\Projects\Blog-Skeleton\Blog-Skeleton\Blog.UI.Tests\Blog.UI.Tests.csproj" /out:"C:\Users\vaneto\Documents\Visual Studio 2015\Projects\Blog-Skeleton\Blog-Skeleton\Blog.UI.Tests\TestResult.htm"


@echo The file paths here are relative and are adjusted for these commands to be run in TeamCityBlogProject's directory in Command prompt on any computer:

packages\NUnit.ConsoleRunner.3.6.1\tools\nunit3-console.exe --labels=All --out=Blog.UI.Tests\TestResult.txt --result=Blog.UI.Tests\TestResult.xml;format=nunit2 Blog.UI.Tests\bin\Debug\Blog.UI.Tests.dll
packages\ReportUnit.1.5.0-beta1\tools\reportunit.exe Blog.UI.Tests\TestResult.xml Blog.UI.Tests\TestResult.html
copy Blog.UI.Tests\TestResult.xml packages\SpecFlow.2.1.0\tools\TestResult.xml
packages\SpecFlow.2.1.0\tools\specflow.exe nunitexecutionreport Blog.UI.Tests\Blog.UI.Tests.csproj /out:Blog.UI.Tests\TestResult.htm