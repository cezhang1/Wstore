Feature: Interaction_Discover

A short summary of the feature
Background: 
@Interaction
Scenario Template: test0_open_ldpp_exe_discover_interaction
	Given Open Ldpp App Window <exe_path>
	And Add Waiting Time <sleep_time1>
	When Click Sign In Button
Scenarios:
    | exe_path                                                        | sleep_time1 |
    | C:\\DCLDPP\\net6.0-windows10.0.19041\\UnitySampleAppWindows.exe | 500         |

@Interaction
# open main test button,and open connect and discover UI
Scenario Template: test001_open_window_and_test_discover_interaction
	When Choose <window_name> from The Following Windows Options And Click Test Button 
Scenarios:
    | window_name |
    |             |

@Interaction
Scenario Template: test002_open_window_and_click_start_button_discover_interaction
	When Click Start Button on The <window_name> Window
	Then The response is expected <expected_response> in <require_approve> under <require_verify_pwd> after <verify_whole_response> 
Scenarios:
    | window_name | expected_response                         | require_approve | require_verify_pwd | verify_whole_response |
    | connect     | "plugin \| Plugin started successfully!!" | true            | false              | 1                     |

@Interaction
Scenario Template: test003_open_window_and_click_start_button_discover_interaction
	When Click Start Button on The <window_name> Window
	Then The response is expected <expected_response> in <require_approve> under <require_verify_pwd> after <verify_whole_response> 
Scenarios:
    | window_name | expected_response                         | require_approve | require_verify_pwd | verify_whole_response |
    | discover    | "plugin \| Plugin started successfully!!" | true            | false              | 1                     |

#接口说明
#connectui Input Command And Press Send ：connect ui 输入命令
#Discoverui Input Command And Press Send：Discover ui 输入指令

@Interaction
Scenario: test004_OrderID_032_"Connection"_verify connection
	When Discoverui Input Command And Press Send
	| command_one | command_two            |
	| Start       | {"IsHomeGroup":"TRUE"} |
	When connectui Input Command And Press Send
	| command_one | command_two    |
	| Start       | {"Status":"0"} |
	When connectui Input Command And Press Send 
	| command_one | command_two |
	| CreateAP    | randomPwd   |
	When Discoverui Input Command And Press Send
	| command_one   | command_two    |
	| StartDiscover | {"Status":"0"} |
	When get file length 
	| command_one | command_two    |
	When User set wifi bluetooth mobilehotspot network connect status
	| settings_name | open  |
	| bluetooth     | false |
	When Discoverui Input Command And Press Send
	| command_one  | command_two  |
	| ReadyForLink | readyforlink |
	Then The response is expected 
    | expected_response                                                                                  | require_approve | require_verify_pwd | verify_whole_response |
    | "{"ResponseCode" : "0", "Message" : "PREPARE_LINK_OK", "Payload":"{"Status": 0 , "Message": ""}"}" | true            | false              | 1                     |
	When is connect status
	| connect status |
	| False          |
	When User set wifi bluetooth mobilehotspot network connect status
	| settings_name | open |
	| bluetooth     | true |

@Interaction
Scenario: test005_OrderID_033_"Connection"_verify connection
    When connectui Input Command And Press Send
	| command_one | command_two    |
	| CloseAP     | {"Status":"0"} |
	When Discoverui Input Command And Press Send
	| command_one  | command_two    |
	| StopDiscover | {"Status":"0"} |
	When connectui Input Command And Press Send 
	| command_one | command_two |
	| CreateAP    | randomPwd   |
	When User set wifi bluetooth mobilehotspot network connect status
	| settings_name | open  |
	| bluetooth     | false |
	When Discoverui Input Command And Press Send
	| command_one   | command_two    |
	| StartDiscover | {"Status":"0"} |
	Then The response is expected 
    | expected_response                                                                                                        | require_approve | require_verify_pwd | verify_whole_response |
    | "{"ResponseCode" : "0", "Message" : "START_DISCOVER_OK", "Payload":"{"Status": 3 , "Message": "Start Discover Error"}"}" | true            | false              | 1                     |
	When User set wifi bluetooth mobilehotspot network connect status
	| settings_name | open |
	| bluetooth     | true |

@Interaction
Scenario: test006_OrderID_034_"Connection"_verify connection
	When connectui Input Command And Press Send
	| command_one | command_two    |
	| CloseAP     | {"Status":"0"} |
	When connectui Input Command And Press Send 
	| command_one | command_two |
	| CreateAP    | randomPwd   |
	When Discoverui Input Command And Press Send
	| command_one   | command_two    |
	| StartDiscover | {"Status":"0"} |
	When get file length 
	| command_one | command_two    |
	When User set wifi bluetooth mobilehotspot network connect status
	| settings_name | open  |
	| wifi          | false |
	When Discoverui Input Command And Press Send
	| command_one  | command_two  |
	| ReadyForLink | readyforlink |	
	Then The response is expected 
    | expected_response                                                                                  | require_approve | require_verify_pwd | verify_whole_response |
    | "{"ResponseCode" : "0", "Message" : "PREPARE_LINK_OK", "Payload":"{"Status": 0 , "Message": ""}"}" | true            | false              | 1                     |
	When is connect status
	| connect status |
	| False          |
	When User set wifi bluetooth mobilehotspot network connect status
	| settings_name | open |
	| wifi          | true |

@Interaction
Scenario: test007_OrderID_035_"Connection"_verify connection
	When connectui Input Command And Press Send
	| command_one | command_two    |
	| CloseAP     | {"Status":"0"} |
	When Discoverui Input Command And Press Send
	| command_one  | command_two    |
	| StopDiscover | {"Status":"0"} |
	When connectui Input Command And Press Send 
	| command_one | command_two |
	| CreateAP    | randomPwd   |
	When Discoverui Input Command And Press Send
	| command_one   | command_two    |
	| StartDiscover | {"Status":"0"} |
	When get file length 
	| command_one | command_two    |
	When Discoverui Input Command And Press Send
	| command_one  | command_two  |
	| ReadyForLink | readyforlink |	
	Then The response is expected 
    | expected_response                                                                                  | require_approve | require_verify_pwd | verify_whole_response |
    | "{"ResponseCode" : "0", "Message" : "PREPARE_LINK_OK", "Payload":"{"Status": 0 , "Message": ""}"}" | true            | false              | 1                     |
	When is connect status
	| connect status |
	| True           |
	When User set wifi bluetooth mobilehotspot network connect status
	| settings_name | open  |
	| wifi          | false |
	#ping不通则连接断开False,ping 通则连接未断开True，wifi关闭，设备连接断开ping不通
	When ping phone IP address
	| command_one | expected_response |
	| ping        | False             |
	When User set wifi bluetooth mobilehotspot network connect status
	| settings_name | open |
	| wifi          | true |

@Interaction
Scenario: test008_OrderID_036_"Connection"_verify connection
	When connectui Input Command And Press Send
	| command_one | command_two    |
	| CloseAP     | {"Status":"0"} |
	When Discoverui Input Command And Press Send
	| command_one  | command_two    |
	| StopDiscover | {"Status":"0"} |
	When connectui Input Command And Press Send 
	| command_one | command_two |
	| CreateAP    | randomPwd   |
	When Discoverui Input Command And Press Send
	| command_one   | command_two    |
	| StartDiscover | {"Status":"0"} |
	When get file length 
	| command_one | command_two    |
	When Discoverui Input Command And Press Send
	| command_one  | command_two  |
	| ReadyForLink | readyforlink |	
	Then The response is expected 
    | expected_response                                                                                  | require_approve | require_verify_pwd | verify_whole_response |
    | "{"ResponseCode" : "0", "Message" : "PREPARE_LINK_OK", "Payload":"{"Status": 0 , "Message": ""}"}" | true            | false              | 1                     |
	When is connect status
	| connect status |
	| True           |
	When User set wifi bluetooth mobilehotspot network connect status
	| settings_name | open  |
	| bluetooth     | false |
	#ping不通则连接断开False,ping 通则连接未断开True,关闭PC蓝牙，设备不会断开
	When ping phone IP address
	| command_one | expected_response |
	| ping        | True              |
	When User set wifi bluetooth mobilehotspot network connect status
	| settings_name | open |
	| bluetooth     | true |

@Interaction	
Scenario: test009_Stop
	When Discoverui Input Command And Press Send
	| command_one  | command_two    |
	| StopDiscover | {"Status":"0"} |
	When connectui Input Command And Press Send
	| command_one | command_two    |
	| CloseAP     | {"Status":"0"} |
	When Discoverui Input Command And Press Send
	| command_one | command_two    |
	| Stop        | {"Status":"0"} |
	When connectui Input Command And Press Send
	| command_one | command_two    |
	| Stop        | {"Status":"0"} |

@Interaction
#connect ui stop button
Scenario: test010_close_connect_button
 	When Click Stop Button on The window_name
	| window_name |
	| connect     |

@Interaction
#discover ui stop button
Scenario: test011_close_discover_button
	When Click Stop Button on The window_name
	| window_name |
	| discover    |

@Interaction
#关闭connect窗口
Scenario: test012_close_connect
	When close connect or discover UI
	| window_name |
	| connect     |

@Interaction
#关闭discover窗口
Scenario: test013_close_discover
	When close connect or discover UI
	| window_name |
	| discover    |

@Interaction
#关闭主窗口
Scenario: test014_close_unity
	When close connect or discover UI
	| window_name |
	| unity       |

