﻿using System;

namespace WebEditor.Models
{
     public class Output
     {
          public Boolean status { get; set; }
          public TimeSpan executionTime { get; set; }
          public string message { get; set; }

     }
}