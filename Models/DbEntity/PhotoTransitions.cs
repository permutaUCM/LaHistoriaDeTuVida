using System.Collections;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace LHDTV.Models.DbEntity
{
    public class PhotoTransition
    {
        public int Id { get; set; }
        public string TransitionName { get; set; }
        public string TransitionUserName { get; set; }
        public int DefaultTransitionTime { get; set; }
        public bool DefaultTransitionAutoStart { get; set; }
    }
}