﻿namespace StudentsApp.Client.Models
{
    public class ApiResponse
    {
        public bool Data { get; set; }
        public bool Succeeded { get; set; }
        public string Message { get; set; }
        public List<string> Errors { get; set; }

        //public string Title { get; set; }
        //public int Status { get; set; }
        //public IReadOnlyDictionary<string, string[]> Errors { get; set; }
    }
}
