using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Practice1
{
    public static class MenuTool
    {
        public static Type[] NativeTypes = {typeof(int), typeof(String), typeof(string), 
            typeof(float), typeof(bool), typeof(char), typeof(double) }; 
        public static String ParametersInRow(ParameterInfo[] parameters)
        {
            return String.Join(", ", parameters.Select(p => p.ParameterType.Name + " " + p.Name).ToArray());
        }

        public static String BeautifyFunction(String name, ParameterInfo[] parameters)
        {
            return name + "(" + ParametersInRow(parameters) + ")";
        }
    }
}