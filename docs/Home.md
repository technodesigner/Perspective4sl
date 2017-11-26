**Project Description**
Perspective is an experimental and pedagogical .NET class library for building 2D and 3D Silverlight applications.

NEW ! [Perspective 3 (3D) for Silverlight 5](http://perspective4sl.codeplex.com/releases)

![](Home_http://www.odewit.net//Articles/Sl3DIntro/BitmapTexture.png)

Parts of the source code of Perspective are used as examples in [my book about Silverlight](http://www.editions-eni.fr/Livres/Silverlight-2-Developpez-des-applications-Internet-riches/.4_3a6222cf-b921-41f5-886c-c989f77ba994_d22f5352-7bd2-4949-9b95-c29149375af9_1_0_d9bd8b5e-f324-473f-b1fc-b41b421c950f.html) (in french).

Which Silverlight version do you use ?
* For Silverlight 5.0, please use [last Perspective version](http://perspective4sl.codeplex.com/releases)
* For Silverlight 4.0, please use [Perspective version 2.0](http://perspective4sl.codeplex.com/releases/view/59737)

Features :
* An application which can dynamically load pages from extension packages (.xap), using an extension system described [here](http://www.odewit.net/ArticleContent.aspx?id=SlExtensions&format=html). This application is used here as demo of the Perspective library. It is localized (english / french).
* IsolatedStorageHelper : An helper class for isolated storage operations.
* StringProviderBase : an abstract class to bind strings from resx files and propagate a culture change through binding.
* StringFormatConverter : a string formatter for databinding.
* SignalBinding : a binding class which throws conversion events, and prevents to write converter classes. The same one than in Perspective for WPF.
* Custom shapes : RegularPolygon, Star, Arrow, Checkerboard and PieSlice ("camembert" in French). 
* Knob : a rotative button control (the same one than in Perspective for WPF). Works like a slider, but is more compact and is compatible with  multiselection.
* BeePanel : a custom panel using an hexagonal layout. Children elements are wrapped.
* BeeGrid : a custom Grid using an hexagonal layout. Children elements use a Row / Column layout system.
* Classes to store captured WAV audio data : WavAudioEncoder and StreamAudioSink.
* MayaEase : a custom easing animation that accelerates and/or decelerates using a maya pyramid-like function.
* Helper classes to handle 2D and 3D matrices.
* A high-level 3D framework for Silverlight 5, similar to Perspective 3D for WPF.

Documentation and demos :
* [Perspective for Silverlight articles](http://www.odewit.net/ArticleList.aspx?key=Perspective4sl)
	* [Perspective : Easy 3D programming with Silverlight 5](http://www.odewit.net/ArticleContent.aspx?id=Sl3DIntro&format=html)
	* [Perspective : Basic 3D models for Silverlight 5](http://www.odewit.net/ArticleContent.aspx?id=Sl3DModels&format=html)
	* [Perspective for Silverlight : dynamic 3D scènes](http://www.odewit.net/ArticleContent.aspx?id=Sl3DDynamicScene)
	* [Dynamic modules and pages management of a Silverlight 4 application](http://www.odewit.net/ArticleContent.aspx?id=SlExtensions&format=html)

Enjoy ! 

Olivier Dewit
[technodesign.solutions](technodesign.solutions)
[@technodesigner](twitter.com/technodesigner)