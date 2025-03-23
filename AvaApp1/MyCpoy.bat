if not exist ".\bin\Debug\net9.0\src\Views" mkdir ".\bin\Debug\net9.0\src\Views"
if not exist ".\bin\Debug\net9.0\src\ViewModels" mkdir ".\bin\Debug\net9.0\src\ViewModels"

xCopy  .\Views  .\bin\Debug\net9.0\src\Views   /s /y 
xCopy  .\ViewModels  .\bin\Debug\net9.0\src\ViewModels  /s /y