using SlackConnection.Interfaces;
using System.Threading.Tasks;

namespace SlackServices
{
    public class HomePublishService
    {
		const string ViewString = @"{
   ""user_id"":""#USERID#"",
   ""view"":{
      ""type"":""home"",
      ""blocks"":[
         {
            ""type"":""image"",
            ""title"":{
               ""type"":""plain_text"",
               ""text"":""Purple Cubed"",
               ""emoji"":true
            },
            ""image_url"":""https://pbs.twimg.com/profile_images/879273391416541184/xivQgR5v_400x400.jpg"",
            ""alt_text"":""Purple Cubed Logo""
         },
         {
            ""type"":""header"",
            ""text"":{
               ""type"":""plain_text"",
               ""text"":""Aim"",
               ""emoji"":true
            }
         },
         {
            ""type"":""section"",
            ""text"":{
               ""type"":""mrkdwn"",
               ""text"":""Create something which at least display’s some information unique to the user. Find the limitations and detail them here please. \r\n\r\n It’d be amazing if you could, say, have a dropdown for a happiness score, and use this tab to load and edit this value for the user (which would need to call an API endpoint to do the save)""
            }
         },
         {
            ""type"":""header"",
            ""text"":{
               ""type"":""plain_text"",
               ""text"":""Your Happiness"",
               ""emoji"":true
            }
         },
         {
            ""type"":""section"",
            ""text"":{
               ""type"":""mrkdwn"",
               ""text"":""How happy are you?""
            },
            ""accessory"":{
               ""type"":""static_select"",
               ""placeholder"":{
                  ""type"":""plain_text"",
                  ""text"":""Score yourself 1 - 10"",
                  ""emoji"":true
               },
               ""options"":[
                  {
                     ""text"":{
                        ""type"":""plain_text"",
                        ""text"":""1"",
                        ""emoji"":true
                     },
                     ""value"":""1""
                  },
                  {
                     ""text"":{
                        ""type"":""plain_text"",
                        ""text"":""2"",
                        ""emoji"":true
                     },
                     ""value"":""2""
                  },
                  {
                     ""text"":{
                        ""type"":""plain_text"",
                        ""text"":""3"",
                        ""emoji"":true
                     },
                     ""value"":""3""
                  },
                  {
                     ""text"":{
                        ""type"":""plain_text"",
                        ""text"":""4"",
                        ""emoji"":true
                     },
                     ""value"":""4""
                  },
                  {
                     ""text"":{
                        ""type"":""plain_text"",
                        ""text"":""5"",
                        ""emoji"":true
                     },
                     ""value"":""5""
                  },
                  {
                     ""text"":{
                        ""type"":""plain_text"",
                        ""text"":""6"",
                        ""emoji"":true
                     },
                     ""value"":""6""
                  },
                  {
                     ""text"":{
                        ""type"":""plain_text"",
                        ""text"":""7"",
                        ""emoji"":true
                     },
                     ""value"":""7""
                  },
                  {
                     ""text"":{
                        ""type"":""plain_text"",
                        ""text"":""8"",
                        ""emoji"":true
                     },
                     ""value"":""8""
                  },
                  {
                     ""text"":{
                        ""type"":""plain_text"",
                        ""text"":""9"",
                        ""emoji"":true
                     },
                     ""value"":""9""
                  },
                  {
                     ""text"":{
                        ""type"":""plain_text"",
                        ""text"":""10"",
                        ""emoji"":true
                     },
                     ""value"":""10""
                  }
               ],
               ""action_id"":""static_select-action""
            }
         },
         {
            ""dispatch_action"":true,
            ""type"":""input"",
            ""element"":{
               ""type"":""plain_text_input"",
               ""action_id"":""plain_text_input-action""
            },
            ""label"":{
               ""type"":""plain_text"",
               ""text"":""Label 1"",
               ""emoji"":true
            }
         },
         {
            ""dispatch_action"":true,
            ""type"":""input"",
            ""element"":{
               ""type"":""plain_text_input"",
               ""dispatch_action_config"":{
                  ""trigger_actions_on"":[
                     ""on_character_entered""
                  ]
               },
               ""action_id"":""plain_text_input-action""
            },
            ""label"":{
               ""type"":""plain_text"",
               ""text"":""Label 2"",
               ""emoji"":true
            }
         },
         {
            ""dispatch_action"":true,
            ""type"":""input"",
            ""element"":{
               ""type"":""multi_users_select"",
               ""placeholder"":{
                  ""type"":""plain_text"",
                  ""text"":""Select users"",
                  ""emoji"":true
               },
               ""action_id"":""multi_users_select-action""
            },
            ""label"":{
               ""type"":""plain_text"",
               ""text"":""Label 5"",
               ""emoji"":true
            }
         },
         {
            ""dispatch_action"":true,
            ""type"":""input"",
            ""element"":{
               ""type"":""static_select"",
               ""placeholder"":{
                  ""type"":""plain_text"",
                  ""text"":""Select an item"",
                  ""emoji"":true
               },
               ""options"":[
                  {
                     ""text"":{
                        ""type"":""plain_text"",
                        ""text"":""*this is plain_text text*"",
                        ""emoji"":true
                     },
                     ""value"":""value-0""
                  },
                  {
                     ""text"":{
                        ""type"":""plain_text"",
                        ""text"":""*this is plain_text text*"",
                        ""emoji"":true
                     },
                     ""value"":""value-1""
                  },
                  {
                     ""text"":{
                        ""type"":""plain_text"",
                        ""text"":""*this is plain_text text*"",
                        ""emoji"":true
                     },
                     ""value"":""value-2""
                  }
               ],
               ""action_id"":""static_select-action""
            },
            ""label"":{
               ""type"":""plain_text"",
               ""text"":""Label 6"",
               ""emoji"":true
            }
         },
         {
            ""type"":""input"",
            ""element"":{
               ""type"":""datepicker"",
               ""initial_date"":""1990-04-28"",
               ""placeholder"":{
                  ""type"":""plain_text"",
                  ""text"":""Select a date"",
                  ""emoji"":true
               },
               ""action_id"":""datepicker-action""
            },
            ""label"":{
               ""type"":""plain_text"",
               ""text"":""Label 7"",
               ""emoji"":true
            }
         },
         {
            ""dispatch_action"":true,
            ""type"":""input"",
            ""element"":{
               ""type"":""checkboxes"",
               ""options"":[
                  {
                     ""text"":{
                        ""type"":""plain_text"",
                        ""text"":""*this is plain_text text*"",
                        ""emoji"":true
                     },
                     ""value"":""value-0""
                  },
                  {
                     ""text"":{
                        ""type"":""plain_text"",
                        ""text"":""*this is plain_text text*"",
                        ""emoji"":true
                     },
                     ""value"":""value-1""
                  },
                  {
                     ""text"":{
                        ""type"":""plain_text"",
                        ""text"":""*this is plain_text text*"",
                        ""emoji"":true
                     },
                     ""value"":""value-2""
                  }
               ],
               ""action_id"":""checkboxes-action""
            },
            ""label"":{
               ""type"":""plain_text"",
               ""text"":""Label 8"",
               ""emoji"":true
            }
         },
         {
            ""dispatch_action"":true,
            ""type"":""input"",
            ""element"":{
               ""type"":""radio_buttons"",
               ""options"":[
                  {
                     ""text"":{
                        ""type"":""plain_text"",
                        ""text"":""*this is plain_text text*"",
                        ""emoji"":true
                     },
                     ""value"":""value-0""
                  },
                  {
                     ""text"":{
                        ""type"":""plain_text"",
                        ""text"":""*this is plain_text text*"",
                        ""emoji"":true
                     },
                     ""value"":""value-1""
                  },
                  {
                     ""text"":{
                        ""type"":""plain_text"",
                        ""text"":""*this is plain_text text*"",
                        ""emoji"":true
                     },
                     ""value"":""value-2""
                  }
               ],
               ""action_id"":""radio_buttons-action"",
               ""response_url_enabled"":true
            },
            ""label"":{
               ""type"":""plain_text"",
               ""text"":""Label 9"",
               ""emoji"":true
            }
         }
      ]
   }
}";

		private readonly ISlackHttpClientService _clientService;

        public HomePublishService(ISlackHttpClientService clientService)
        {
            _clientService = clientService;
        }

        public async Task PublishUserHomeTab(string userId)
        {
            var json = ViewString.Replace("#USERID#", userId);
			await _clientService.PublishViewAsync(json);
        }
    }
}
