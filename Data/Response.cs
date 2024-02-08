using System;
using Microsoft.AspNetCore.Identity;
using rosterapi.Models;

namespace rosterapi.Data
{
	public class Response
	{
        public string? Status { get; set; }
        public string? Message { get; set; }
        public List<IdentityError> Errors { get; set; }
    }
}

