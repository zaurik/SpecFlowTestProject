
#### CI/CD Tool
[Jenkins](https://www.jenkins.io/)

#### Node Setup/Preparation
In order to run the tests on a Jenkins node, [Build Tools for Visual Studio 2022](https://visualstudio.microsoft.com/downloads/?q=build+tools#build-tools-for-visual-studio-2022) ([mirror link](https://aka.ms/vs/17/release/vs_BuildTools.exe)) would have to be installed on the node(s).

#### Tool Setup

 1. Install Jenkins
 2. Install the [MSBuild](https://plugins.jenkins.io/msbuild/) plugin
 3. In Jenkins, go to **Manage Jenkins** > **Global Tool Configuration**
 4. Scroll down to the **MSBuild** section
 5. Click on the **Add MSBuild** button
 6. Provide a name for the instance (**e.g.-** MSBuild 2022)
 7. In the **Path to MSBuild** textbox, give the path to MSBuild on the node which was installed via **Build Tools for Visual Studio 2022** (**e.g.-** C:\Program Files\Microsoft Visual Studio\2022\BuildTools\MSBuild\Current\Bin\MSBuild.exe)
 8. Click on the **Save** button

#### Creating the Pipeline

 1. Log in to Jenkins
 2. Click on **New Item**
 3. Enter a name for the pipeline, click on **Freestyle project** and click on the **OK** button
 4. In the **Configure** page, specify the node(s) to be used, Source Code Management and Build Triggers as required
 5. Click on the **Add build step** button and click on the **Build a Visual Studio project or solution using MSBuild** option - This action will build the solution.
 6. In the **Build a Visual Studio project or solution using MSBuild** section, choose the instance name created earlier from the dropdown
 7. For the **MSBuild Build File** textbox, enter the Visual Studio Solution file name (**i.e.** SpecFlowTestProject.sln)
 8. For the **Command Line Arguments** textbox, enter `/p:Configuration='Debug'`
 9. Click on the **Add build step** button and click on the **Execute Windows batch command** option -  This will be used to execute the tests
 10. In the **Execute Windows batch command** section, enter the the path to **vstest.console.exe** which was installed via **Build Tools for Visual Studio 2022** followed by the path (relative to the solution file) to the test DLLs (**e.g.-** `"C:\Program Files\Microsoft Visual Studio\2022\Community\Common7\IDE\CommonExtensions\Microsoft\TestWindow\vstest.console.exe" "SpecFlowTestProject\bin\Debug\net6.0\SpecFlowTestProject.dll"`
 11. Click on the **Save** button
