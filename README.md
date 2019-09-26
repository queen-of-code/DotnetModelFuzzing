# Introduction 
Options for fuzz testing on dotnet core are somewhat limited - especially when you want to provide a data model that is not necessarily file or I/O based. Thus, enter Dotnet Model Fuzzing! Based on the attacks from FuzzDB, this package allows you to provide a model of your choice, generate an attack strategy based on that model, and then quickly apply fuzz attacks to that provided data model.

This allows you to provide sane templates for your data, such that your fuzz attacks will have a much higher likelyhood of testing code you actually care about. When you're running an HTTP service, testing IIS or Apache isn't all that useful compared to the benefit you would gain from testing your application-specific code!

# Getting Started
TODO: Use the soon-to-be-published nuget package!

For examples on how to get started, take a look at the simple HTTP request application in ExampleApp.

# Contribute
All contributions welcome. 

# Further Reading
* https://github.com/fuzzdb-project/fuzzdb
* https://www.fuzzingbook.org/html/APIFuzzer.html
