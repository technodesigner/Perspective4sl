Perspective 3.0 library for Silverlight
---------------------------------------

This file describes the solution and its new functionalities since the last official release (2.0).

General
-------
- The latest release can be downloaded here : http://perspective4sl.codeplex.com/.
- Perspective 3.0 is for Silverlight 5.0 only ; it doesn't support directly older Silverlight versions 
(although some code may be compatible).

perspective4sl.sl5.sln
----------------------
- Because of the Perspective extension system, it is required to generate Perspective.Core.sl5, Perspective.Hosting.sl5, Perspective.Wpf.sl5 and Perspective.Wpf3DX.sl5 before generating the first time the solution.
- If you use Visual Web Developer Express, you can ignore the warnings at the first solution loading.

Perspective application project
-------------------------------
A Silverlight application which can dynamically load pages from extension XAP, 
using an extension system described here : http://www.odewit.net/ArticleContent.aspx?id=SlExtensions&format=html. 
This application is used here as demo of Perspective library. It is localized (english / french).
It uses Silverlight Toolkit (http://silverlight.codeplex.com).
It requires elevated trust when running in browser, because of new 3D security model (for PerspectiveDemo3D assembly).
To display 3D models in OOB mode, you must first allow this permission in Silverlight configuration in browser : "3D graphics: use blocked display drivers".

Perspective.Web
---------------
The web host application.
- Define Perspective.Web as startup project.
- Define PerspectiveDemoApp.html as start page.

Perspective.Config
------------------
An extension assembly to manage the configuration for the Perspective application.

PerspectiveDemo
---------------
An extension assembly which is a demo of Perspective library features.

PerspectiveDemo3D
-----------------
An extension assembly which is a demo of Perspective library 3D features.

Perspective.Hosting
-------------------
- A Silverlight project to manage the modules (extensions) of the Perspective application project. 
- The classes are used by the entension assemblies.
- Extension : Represents an extension for the Perspective application.
- PageLink : Represents the metadata associated with a Silverlight page of an extension.

Perspective.Core
----------------
- Core features.

Perspective.Wpf
---------------
- General and 2D features.

Perspective.Wpf3DX
------------------
- A new project : Perspective 3D for Silverlight 5.
- The Arrow model has been renamed as Dart (because there is already a 2D Arrow class in Perspective). Default ratio of the head to the first unit is 0.8 (instead of 0.2 for the WPF equivalent Arrow model).
