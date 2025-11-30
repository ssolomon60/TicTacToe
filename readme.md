Overview

This project is part of my ongoing effort to strengthen my skills as a software engineer by building full systems that combine backend architecture, file handling, and object-oriented design. I wanted to create software that demonstrates my understanding of C# syntax, abstraction, inheritance, and working with APIs while also simulating a real-world application scenario.

The software I built is a Tic-Tac-Toe backend system written in C#. It supports game history tracking, difficulty settings, score persistence, JSON file storage, and a clean API structure. The system uses an abstract base class to support multiple record types, and a service layer to manage file I/O operations. While this backend can support any UI, the focus here is entirely on the C# implementation and backend logic.

My purpose in writing this software was to practice building structured C# applications that include controllers, models, services, inheritance, and file serialization. This helped me gain a deeper understanding of how professional backends are organized and how data flows through a C# web API.

Software Demo Video

Development Environment

I used Visual Studio Code as the primary development environment along with the .NET 8 SDK to run and test the C# Web API.

The application is written entirely in C# using ASP.NET Core Web API.
Key features of the environment include:

C# 10+ language features

ASP.NET Core controller routing

JSON serialization with System.Text.Json

Dependency Injection for service management

Local JSON file persistence

No external libraries were used beyond those included with .NET.

Useful Websites

These resources were helpful while working on this project:

.NET Documentation

ASP.NET Core Web API Tutorial

C# Programming Guide

JSON Serialization in C#

Future Work

Here are some improvements I plan to add in the future:

Add a real AI opponent using minimax for advanced difficulty

Expand the backend to support multiple users and profiles

Add authentication for storing individual player stats

Move storage from JSON files to a database (SQL or NoSQL)

Add a leaderboard system with ranking rules

Improve validation and error handling throughout the API