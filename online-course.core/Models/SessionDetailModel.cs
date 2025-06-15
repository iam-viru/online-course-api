﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace online_course.core.Models
{

    //this will brings all the details of a session for a given course
    public class SessionDetailModel
    {
        public int SessionId { get; set; }

        public int CourseId { get; set; }

        public string Title { get; set; } = null!;

        public string? Description { get; set; }

        public string? VideoUrl { get; set; }

        public int VideoOrder { get; set; }
    }
}
