using System;
using System.Collections.Generic;
using LHDTV.Service;
using LHDTV.Models.Forms;
using LHDTV.Models.DbEntity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;


namespace LHDTV.Controllers
{
    [ApiController]
    [Route("api/administration")]
    [Authorize(Roles = "ADMIN")]
    public class AdministrationController : ControllerBase
    {
        [HttpGet("tagMaster")]
        public IActionResult getTagMaster()
        {

            return Ok(
                new
                {
                    Metadata = new
                    {
                        Extras = new List<Extra>(){
                            new Extra()
                            {
                                Name = "Ex1",
                                type = "ratio",
                                extras = "5"

                            },

                            new Extra()
                            {
                                Name = "Ex2",
                                type = "ratio",
                                extras = "5"

                            }

                        }
                    },
                    Data = new List<LHDTV.Models.DbEntity.PhotoTagsTypes>(){
                    new PhotoTagsTypes()
        {
            Name = "name1",
                        Extra1 = new Extra()
                        {
                            Name = "nameExtra1",
                            type = "ratio",
                            extras = ""
                        },
                        Extra2 = null,
                        Extra3 = null
                    },new PhotoTagsTypes()
        {
            Name = "name2",
                        Extra1 = new Extra()
                        {
                            Name = "nameExtra1",
                            type = "ratio",
                            extras = ""
                        },
                        Extra2 = null,
                        Extra3 = null
                    },new PhotoTagsTypes()
        {
            Name = "name3",
                        Extra1 = new Extra()
                        {
                            Name = "nameExtra1",
                            type = "ratio",
                            extras = ""
                        },
                        Extra2 = null,
                        Extra3 = null
                    },new PhotoTagsTypes()
        {
            Name = "name4",
                        Extra1 = new Extra()
                        {
                            Name = "nameExtra1",
                            type = "ratio",
                            extras = ""
                        },
                        Extra2 = null,
                        Extra3 = null
                    },new PhotoTagsTypes()
        {
            Name = "name5",
                        Extra1 = new Extra()
                        {
                            Name = "nameExtra1",
                            type = "ratio",
                            extras = ""
                        },
                        Extra2 = null,
                        Extra3 = null
                    },new PhotoTagsTypes()
        {
            Name = "name6",
                        Extra1 = new Extra()
                        {
                            Name = "nameExtra1",
                            type = "ratio",
                            extras = ""
                        },
                        Extra2 = null,
                        Extra3 = null
                    },new PhotoTagsTypes()
        {
            Name = "name7",
                        Extra1 = new Extra()
                        {
                            Name = "nameExtra1",
                            type = "ratio",
                            extras = ""
                        },
                        Extra2 = null,
                        Extra3 = null
                    },new PhotoTagsTypes()
        {
            Name = "name8",
                        Extra1 = new Extra()
                        {
                            Name = "nameExtra1",
                            type = "ratio",
                            extras = ""
                        },
                        Extra2 = null,
                        Extra3 = null
                    },new PhotoTagsTypes()
        {
            Name = "name9",
                        Extra1 = new Extra()
                        {
                            Name = "nameExtra1",
                            type = "ratio",
                            extras = ""
                        },
                        Extra2 = null,
                        Extra3 = null
                    },new PhotoTagsTypes()
        {
            Name = "name10",
                        Extra1 = new Extra()
                        {
                            Name = "nameExtra1",
                            type = "ratio",
                            extras = ""
                        },
                        Extra2 = null,
                        Extra3 = null
                    },new PhotoTagsTypes()
        {
            Name = "name11",
                        Extra1 = new Extra()
                        {
                            Name = "nameExtra1",
                            type = "ratio",
                            extras = ""
                        },
                        Extra2 = null,
                        Extra3 = null
                    },new PhotoTagsTypes()
        {
            Name = "name12",
                        Extra1 = new Extra()
                        {
                            Name = "nameExtra1",
                            type = "ratio",
                            extras = ""
                        },
                        Extra2 = null,
                        Extra3 = null
                    },new PhotoTagsTypes()
        {
            Name = "name13",
                        Extra1 = new Extra()
                        {
                            Name = "nameExtra1",
                            type = "ratio",
                            extras = ""
                        },
                        Extra2 = null,
                        Extra3 = null
                    }
    }
                }

            );
        }



        [HttpGet("tagMaster/delete/{id}")]
        public IActionResult removeTagMaster(string id)
        {
            return Ok(
                new
                {
                    Metadata = new
                    {
                        Extras = new List<Extra>(){
                            new Extra()
                            {
                                Name = "Ex1",
                                type = "ratio",
                                extras = "5"

                            },

                            new Extra()
                            {
                                Name = "Ex2",
                                type = "ratio",
                                extras = "5"

                            }

                        }
                    },
                    Data = new List<LHDTV.Models.DbEntity.PhotoTagsTypes>(){
                    new PhotoTagsTypes()
        {
            Name = "name1",
                        Extra1 = new Extra()
                        {
                            Name = "nameExtra1",
                            type = "ratio",
                            extras = ""
                        },
                        Extra2 = null,
                        Extra3 = null
                    },new PhotoTagsTypes()
        {
            Name = "name2",
                        Extra1 = new Extra()
                        {
                            Name = "nameExtra1",
                            type = "ratio",
                            extras = ""
                        },
                        Extra2 = null,
                        Extra3 = null
                    },new PhotoTagsTypes()
        {
            Name = "name4",
                        Extra1 = new Extra()
                        {
                            Name = "nameExtra1",
                            type = "ratio",
                            extras = ""
                        },
                        Extra2 = null,
                        Extra3 = null
                    },new PhotoTagsTypes()
        {
            Name = "name5",
                        Extra1 = new Extra()
                        {
                            Name = "nameExtra1",
                            type = "ratio",
                            extras = ""
                        },
                        Extra2 = null,
                        Extra3 = null
                    },new PhotoTagsTypes()
        {
            Name = "name8",
                        Extra1 = new Extra()
                        {
                            Name = "nameExtra1",
                            type = "ratio",
                            extras = ""
                        },
                        Extra2 = null,
                        Extra3 = null
                    },new PhotoTagsTypes()
        {
            Name = "name9",
                        Extra1 = new Extra()
                        {
                            Name = "nameExtra1",
                            type = "ratio",
                            extras = ""
                        },
                        Extra2 = null,
                        Extra3 = null
                    },new PhotoTagsTypes()
        {
            Name = "name10",
                        Extra1 = new Extra()
                        {
                            Name = "nameExtra1",
                            type = "ratio",
                            extras = ""
                        },
                        Extra2 = null,
                        Extra3 = null
                    },new PhotoTagsTypes()
        {
            Name = "name11",
                        Extra1 = new Extra()
                        {
                            Name = "nameExtra1",
                            type = "ratio",
                            extras = ""
                        },
                        Extra2 = null,
                        Extra3 = null
                    },new PhotoTagsTypes()
        {
            Name = "name12",
                        Extra1 = new Extra()
                        {
                            Name = "nameExtra1",
                            type = "ratio",
                            extras = ""
                        },
                        Extra2 = null,
                        Extra3 = null
                    },new PhotoTagsTypes()
        {
            Name = "name13",
                        Extra1 = new Extra()
                        {
                            Name = "nameExtra1",
                            type = "ratio",
                            extras = ""
                        },
                        Extra2 = null,
                        Extra3 = null
                    }
    }
                }

            );

        }
    }
}