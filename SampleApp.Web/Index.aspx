<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="SampleApp1.Index" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml"  lang="en" ng-app="ngdemo">
<head>
    <meta charset="utf-8">
    <title>My AngularJS App</title>
    <link rel="stylesheet" href="css/app.css"/>
    <link rel="stylesheet" href="lib/angular/ng-table.css"/>
    <link rel="stylesheet" href="lib/angular/bootstrap.min.css" /> 
</head>
<body>
    <div ng-view></div>

    <!-- In production use:
    <script src="//ajax.googleapis.com/ajax/libs/angularjs/1.0.7/angular.min.js"></script>
    -->
    <script src="lib/angular/angular.js"></script>
    <script src="lib/angular/angular-route.min.js"></script>
    <script type="text/javascript" src="lib/angular/angular-translate.min.js"></script>
    <script type="text/javascript" src="lib/angular/angular-translate-loader-static-files.min.js"></script>
    <script src="lib/angular/ui-bootstrap-tpls.min.js"></script>
    <!-- <script src="lib/angular/angular-resource.js"></script>  -->
    <script type="text/javascript" src="lib/angular/ng-table.js"></script>
    <script src="js/app.js"></script>
    <script src="js/services.js"></script>
    <script src="js/controllers.js"></script>
    <script src="js/filters.js"></script>
    <script src="js/directives.js"></script>
</body>
</html>
