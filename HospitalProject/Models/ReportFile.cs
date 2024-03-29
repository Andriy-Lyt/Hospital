﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HospitalProject.Models
{
    public class ReportFile
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public int ReportId { get; set; }

        [ForeignKey("ReportId")]
        public Report ParentReport { get; set; }
    }
}