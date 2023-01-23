# PracticeConsoleFramework
In Source folder we put practice class file,
in Build folder we put built-in practice class that need use in other practice class
in Practice folder we put practice class file

+ every practice class must implement IClass interface. the interface have 2 items:
	+ first one is PrcName property as a string that represent the name of practice for showing in menu
	+ second is Trigger method. it's a trigger for runing practice.
+ Instance class find out practice classes by a naming & location convention.
	+ Built-in class files name begin with Build keyword in Source\Build folder.
	+ Practice class files name begin with Prc keyword in Source\Practice folder.
	+ in this folders, the namespace is specific for Instance class.