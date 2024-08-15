The uploaded project is based on the http://www.nakov.com/blog/2015/05/28/free-asp-net-mvc-hands-on-lab/ tutorial.

___ASP.NET MVC Lab (May 2015) – Web Based Events Management System___

The goal of this lab is to help me learn how to __develop ASP.NET MVC data-driven Web applications__. The created MVC application demonstrates the list / create / edit / delete events. The used development IDE is __Visual Studio 2013__ with the latest available updates + SQL Server 2012. 


___Project Assignment___

Design and implement a __Web based event management system__.
· __Events__ have __title__, __start date__ and optionally __start time__. Events may have also (optionally) __duration, description, location__ and __author__. Events can be __public__ (visible by everyone) and __private__ (visible to their owner author only). Events may have __comments__. __Comments__ belong to certain event and have __content__ (text), __date__ and optional __author__ (owner).

·  __Anonymous__ users (without login) can __view all public events__. The home page displays all public events, in two groups: upcoming and passed. Events are shown in __short form__ (title, date, duration, author and location) and have a __[View Details]__ button, which loads dynamically (by __AJAX__) their description, comments and [Edit] / [Delete] buttons.

· Anonymous users can __register__ in the system and __login / logout__. Users should have mandatory __email, password__ and __full name__. User’s email should be unique. User’s password should be non-empty but can be just one character.

· __Logged-in users__ should be able to __view their own events__, to __create new events, edit their own events__ and __delete their own events__. Deleting events requires a __confirmation__. Implement client-side and sever-side data __validation__.

· A special user “__admin@admin.com__” should have the role “__Administrator__” and should have full permissions to __edit / delete events__ and __comments__.
