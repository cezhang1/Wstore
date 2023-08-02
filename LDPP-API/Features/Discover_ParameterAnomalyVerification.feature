Feature: Discover_ParameterAnomalyVerification


A short summary of the feature
Background: 
@Discover
Scenario Template: test0_open_ldpp_exe_discover_anomaly
	Given Open Ldpp App Window <exe_path>
	And Add Waiting Time <sleep_time1>
	When Click Sign In Button
Scenarios:
    | exe_path                                                        | sleep_time1 |
    | C:\\DCLDPP\\net6.0-windows10.0.19041\\UnitySampleAppWindows.exe | 500         |

@Discover
# open main test button,and open connect and discover UI
Scenario Template: test001_open_window_and_test_discover_anomaly
	When Choose <window_name> from The Following Windows Options And Click Test Button 
Scenarios:
    | window_name |
    |             |

@Discover
Scenario Template: test002_open_window_and_click_start_button_discover_anomaly
	When Click Start Button on The <window_name> Window
	Then The response is expected <expected_response> in <require_approve> under <require_verify_pwd> after <verify_whole_response> 
Scenarios:
    | window_name | expected_response                         | require_approve | require_verify_pwd | verify_whole_response |
    | connect     | "plugin \| Plugin started successfully!!" | true            | false              | 1                     |

@Discover
Scenario Template: test003_open_window_and_click_start_button_discover_anomaly
	When Click Start Button on The <window_name> Window
	Then The response is expected <expected_response> in <require_approve> under <require_verify_pwd> after <verify_whole_response> 
Scenarios:
    | window_name | expected_response                         | require_approve | require_verify_pwd | verify_whole_response |
    | discover    | "plugin \| Plugin started successfully!!" | true            | false              | 1                     |

#接口说明
#connectui Input Command And Press Send ：connect ui 输入命令
#Discoverui Input Command And Press Send：Discover ui 输入指令

@Discover
Scenario: test004_OrderID_007_"Start"_ble module start interface_Missing key "IsHomeGroup"
	When Discoverui Input Command And Press Send
	| command_one | command_two        |
	| Start       | {"IsGroup":"TRUE"} |
	Then The response is expected 
    | expected_response                                                           | require_approve | require_verify_pwd | verify_whole_response |
    | "{"ResponseCode" : "7", "Message" : "START_ERROR_JSON_IS_HOME_GROUP_NULL"}" | true            | false              | 1                     |

@Discover
Scenario: test005_OrderID_008_"Start"_ble module start interface_Invalid value of key "IsHomeGroup"
	When Discoverui Input Command And Press Send
	| command_one | command_two            |
	| Start       | {"IsHomeGroup":"true"} |
	Then The response is expected 
    | expected_response                                                                    | require_approve | require_verify_pwd | verify_whole_response |
    | "{"ResponseCode" : "8", "Message" : "START_ERROR_JSON_IS_HOME_GROUP_INVALID_VALUE"}" | true            | false              | 1                     |

@Discover
Scenario: test006_OrderID_009_"Start"_ble module start interface_Input nothing
	When Discoverui Input Command And Press Send
	| command_one | command_two |
	| Start       |             |
	Then The response is expected 
    | expected_response                                                  | require_approve | require_verify_pwd | verify_whole_response |
    | "{"ResponseCode" : "2", "Message" : "START_ERROR_INPUT_IS_EMPTY"}" | true            | false              | 1                     |

@Discover
Scenario: test007_OrderID_010_"Start"_ble module start interface_Invalid Json Object:null
	When Discoverui Input Command And Press Send
	| command_one | command_two |
	| Start       | null        |
	Then The response is expected 
    | expected_response                                                          | require_approve | require_verify_pwd | verify_whole_response |
    | "{"ResponseCode" : "4", "Message" : "START_ERROR_JSON_DESERIALIZE_ERROR"}" | true            | false              | 1                     |

@Discover
Scenario: test008_OrderID_011_"Start"_ble module start interface_Empty Json Object:{}
	When Discoverui Input Command And Press Send
	| command_one | command_two |
	| Start       | {}       |
	Then The response is expected 
    | expected_response                                                           | require_approve | require_verify_pwd | verify_whole_response |
    | "{"ResponseCode" : "7", "Message" : "START_ERROR_JSON_IS_HOME_GROUP_NULL"}" | true            | false              | 1                     |

@Discover
Scenario: test009_OrderID_012_"Stop"_ble module stop interface_Invalid Json Object:null
	When Discoverui Input Command And Press Send
	| command_one | command_two             |
	| Start       | {"IsHomeGroup":"FALSE"} |
	When Discoverui Input Command And Press Send
	| command_one | command_two |
	| Stop        | null        |
	Then The response is expected 
    | expected_response                                                                     | require_approve | require_verify_pwd | verify_whole_response |
    | "{"ResponseCode" : "4", "Message" : "COMMON_INTERFACE_ERROR_JSON_DESERIALIZE_ERROR"}" | true            | false              | 1                     |

@Discover
Scenario: test010_OrderID_013_"Stop"_ble module stop interface_Input Nothing
	When Discoverui Input Command And Press Send
	| command_one | command_two |
	| Stop        |             |
	Then The response is expected 
    | expected_response                                                             | require_approve | require_verify_pwd | verify_whole_response |
    | "{"ResponseCode" : "2", "Message" : "COMMON_INTERFACE_ERROR_INPUT_IS_EMPTY"}" | true            | false              | 1                     |

@Discover
Scenario: test011_OrderID_014_"Stop"_ble module stop interface_The value of key "Status" is wrong:
	When Discoverui Input Command And Press Send
	| command_one | command_two    |
	| Stop        | {"Status":"1"} |
	Then The response is expected 
    | expected_response                                                                        | require_approve | require_verify_pwd | verify_whole_response |
    | "{"ResponseCode" : "6", "Message" : "COMMON_INTERFACE_ERROR_JSON_INVALID_STATUS_VALUE"}" | true            | false              | 1                     |

@Discover
Scenario: test012_OrderID_015_"Stop"_ble module stop interface_Missing key "Status"
	When Discoverui Input Command And Press Send
	| command_one | command_two  |
	| Stop        | {"Stus":"0"} |
	Then The response is expected 
    | expected_response                                                               | require_approve | require_verify_pwd | verify_whole_response |
    | "{"ResponseCode" : "5", "Message" : "COMMON_INTERFACE_ERROR_JSON_STATUS_NULL"}" | true            | false              | 1                     |

@Discover
Scenario: test013_OrderID_016_"Stop"_ble module stop interface_The value's type of key "Status" is wrong
	When Discoverui Input Command And Press Send
	| command_one | command_two  |
	| Stop        | {"Status":0} |
	Then The response is expected 
    | expected_response                                                                         | require_approve | require_verify_pwd | verify_whole_response |
    | "{"ResponseCode" : "3", "Message" : "COMMON_INTERFACE_ERROR_JSON_DESERIALIZE_EXCEPTION"}" | true            | false              | 1                     |

@Discover
Scenario: test014_OrderID_017_"Stop"_ble module stop interface_Empty Json Object
	When Discoverui Input Command And Press Send
	| command_one | command_two |
	| Stop        | {}          |
	Then The response is expected 
    | expected_response                                                               | require_approve | require_verify_pwd | verify_whole_response |
    | "{"ResponseCode" : "5", "Message" : "COMMON_INTERFACE_ERROR_JSON_STATUS_NULL"}" | true            | false              | 1                     |

@Discover
Scenario: test015_OrderID_018_"StartDiscover"_ble module start scan device interface_Invalid Json Object:null
	When Discoverui Input Command And Press Send
	| command_one   | command_two |
	| StartDiscover | null        |
	Then The response is expected 
    | expected_response                                                                   | require_approve | require_verify_pwd | verify_whole_response |
    | "{"ResponseCode" : "4", "Message" : "START_DISCOVER_ERROR_JSON_DESERIALIZE_ERROR"}" | true            | false              | 1                     |

@Discover
Scenario: test016_OrderID_019_"StartDiscover"_ble module start scan device interface_Input Nothing
	When Discoverui Input Command And Press Send
	| command_one   | command_two |
	| StartDiscover |             |
	Then The response is expected 
    | expected_response                                                           | require_approve | require_verify_pwd | verify_whole_response |
    | "{"ResponseCode" : "2", "Message" : "START_DISCOVER_ERROR_INPUT_IS_EMPTY"}" | true            | false              | 1                     |

@Discover
Scenario: test017_OrderID_020_"StartDiscover"_ble module start scan device interface_The value of key "Status" is wrong:
	When Discoverui Input Command And Press Send
	| command_one   | command_two    |
	| StartDiscover | {"Status":"1"} |
	Then The response is expected 
    | expected_response                                                                      | require_approve | require_verify_pwd | verify_whole_response |
    | "{"ResponseCode" : "6", "Message" : "START_DISCOVER_ERROR_JSON_INVALID_STATUS_VALUE"}" | true            | false              | 1                     |

@Discover
Scenario: test018_OrderID_021_"StartDiscover"_ble module start scan device interface_Missing key "Status"
	When Discoverui Input Command And Press Send
	| command_one   | command_two  |
	| StartDiscover | {"Stus":"0"} |
	Then The response is expected 
    | expected_response                                                             | require_approve | require_verify_pwd | verify_whole_response |
    | "{"ResponseCode" : "5", "Message" : "START_DISCOVER_ERROR_JSON_STATUS_NULL"}" | true            | false              | 1                     |

@Discover
Scenario: test019_OrderID_022_"StartDiscover"_ble module start scan device interface_The value's type of key "Status" is wrong:
	When Discoverui Input Command And Press Send
	| command_one   | command_two  |
	| StartDiscover | {"Status":0} |
	Then The response is expected 
    | expected_response                                                                       | require_approve | require_verify_pwd | verify_whole_response |
    | "{"ResponseCode" : "3", "Message" : "START_DISCOVER_ERROR_JSON_DESERIALIZE_EXCEPTION"}" | true            | false              | 1                     |

@Discover
Scenario: test020_OrderID_023_"StartDiscover"_ble module start scan device interface_Empty Json Object
	When Discoverui Input Command And Press Send
	| command_one   | command_two |
	| StartDiscover | {}          |
	Then The response is expected 
    | expected_response                                                             | require_approve | require_verify_pwd | verify_whole_response |
    | "{"ResponseCode" : "5", "Message" : "START_DISCOVER_ERROR_JSON_STATUS_NULL"}" | true            | false              | 1                     |

@Discover
Scenario: test021_OrderID_024_"GetDevices"_ble module get connected ble devices' information_Invalid Json Object
	When Discoverui Input Command And Press Send
	| command_one | command_two |
	| GetDevices  | null        |
	Then The response is expected 
	| expected_response                                                                     | require_approve | require_verify_pwd | verify_whole_response |
	| "{"ResponseCode" : "4", "Message" : "COMMON_INTERFACE_ERROR_JSON_DESERIALIZE_ERROR"}" | true            | false              | 1                     |

@Discover
Scenario: test022_OrderID_025_"GetDevices"_ble module get connected ble devices' information_Empty Json Object
	When Discoverui Input Command And Press Send
	| command_one | command_two |
	| GetDevices  | {}          |
	Then The response is expected 
	| expected_response                                                               | require_approve | require_verify_pwd | verify_whole_response |
	| "{"ResponseCode" : "5", "Message" : "COMMON_INTERFACE_ERROR_JSON_STATUS_NULL"}" | true            | false              | 1                     |

@Discover
Scenario: test023_OrderID_026_"GetDevices"_ble module get connected ble devices' information_Input nothing
	When Discoverui Input Command And Press Send
	| command_one | command_two |
	| GetDevices  |             |
	Then The response is expected 
	| expected_response                                                             | require_approve | require_verify_pwd | verify_whole_response |
	| "{"ResponseCode" : "2", "Message" : "COMMON_INTERFACE_ERROR_INPUT_IS_EMPTY"}" | true            | false              | 1                     |

@Discover
Scenario: test024_OrderID_027_"GetDevices"_ble module get connected ble devices' information_The value of key "Status" is wrong:
	When Discoverui Input Command And Press Send
	| command_one | command_two    |
	| GetDevices  | {"Status":"1"} |
	Then The response is expected 
	| expected_response                                                                        | require_approve | require_verify_pwd | verify_whole_response |
	| "{"ResponseCode" : "6", "Message" : "COMMON_INTERFACE_ERROR_JSON_INVALID_STATUS_VALUE"}" | true            | false              | 1                     |

@Discover
Scenario: test025_OrderID_028_"GetDevices"_ble module get connected ble devices' information_Missing key "Status":
	When Discoverui Input Command And Press Send
	| command_one | command_two  |
	| GetDevices  | {"Stus":"1"} |
	Then The response is expected 
	| expected_response                                                               | require_approve | require_verify_pwd | verify_whole_response |
	| "{"ResponseCode" : "5", "Message" : "COMMON_INTERFACE_ERROR_JSON_STATUS_NULL"}" | true            | false              | 1                     |

@Discover
Scenario: test026_OrderID_029_"GetDevices"_ble module get connected ble devices' information_The value's type of key "Status" is wrong:
	When Discoverui Input Command And Press Send
	| command_one | command_two  |
	| GetDevices  | {"Status":1} |
	Then The response is expected 
	| expected_response                                                                         | require_approve | require_verify_pwd | verify_whole_response |
	| "{"ResponseCode" : "3", "Message" : "COMMON_INTERFACE_ERROR_JSON_DESERIALIZE_EXCEPTION"}" | true            | false              | 1                     |

@Discover
Scenario: test027_OrderID_030_"GetBTStatus"_ble module get current device's bluetooth's status_Invalid Json Object
	When Discoverui Input Command And Press Send
	| command_one | command_two |
	| GetBTStatus | null        |
	Then The response is expected 
    | expected_response                                                                     | require_approve | require_verify_pwd | verify_whole_response |
    | "{"ResponseCode" : "4", "Message" : "COMMON_INTERFACE_ERROR_JSON_DESERIALIZE_ERROR"}" | true            | false              | 0                     |

@Discover
Scenario: test028_OrderID_031_"GetBTStatus"_ble module get current device's bluetooth's status_Empty Json Object
	When Discoverui Input Command And Press Send
	| command_one | command_two |
	| GetBTStatus | {}          |
	Then The response is expected 
    | expected_response                                                               | require_approve | require_verify_pwd | verify_whole_response |
    | "{"ResponseCode" : "5", "Message" : "COMMON_INTERFACE_ERROR_JSON_STATUS_NULL"}" | true            | false              | 0                     |

@Discover
Scenario: test029_OrderID_032_"GetBTStatus"_ble module get current device's bluetooth's status_Input nothing
	When Discoverui Input Command And Press Send
	| command_one | command_two |
	| GetBTStatus |             |
	Then The response is expected 
    | expected_response                                                             | require_approve | require_verify_pwd | verify_whole_response |
    | "{"ResponseCode" : "2", "Message" : "COMMON_INTERFACE_ERROR_INPUT_IS_EMPTY"}" | true            | false              | 0                     |

@Discover
Scenario: test030_OrderID_033_"GetBTStatus"_ble module get current device's bluetooth's status_The value of key "Status" is wrong
	When Discoverui Input Command And Press Send
	| command_one | command_two    |
	| GetBTStatus | {"Status":"1"} |
	Then The response is expected 
    | expected_response                                                                        | require_approve | require_verify_pwd | verify_whole_response |
    | "{"ResponseCode" : "6", "Message" : "COMMON_INTERFACE_ERROR_JSON_INVALID_STATUS_VALUE"}" | true            | false              | 0                     |

@Discover
Scenario: test031_OrderID_034_"GetBTStatus"_ble module get current device's bluetooth's status_Missing key "Status"
	When Discoverui Input Command And Press Send
	| command_one | command_two  |
	| GetBTStatus | {"Stus":"1"} |
	Then The response is expected 
    | expected_response                                                               | require_approve | require_verify_pwd | verify_whole_response |
    | "{"ResponseCode" : "5", "Message" : "COMMON_INTERFACE_ERROR_JSON_STATUS_NULL"}" | true            | false              | 0                     |

@Discover
Scenario: test032_OrderID_035_"GetBTStatus"_ble module get current device's bluetooth's status_The value's type of key "Status" is wrong
	When Discoverui Input Command And Press Send
	| command_one | command_two  |
	| GetBTStatus | {"Status":1} |
	Then The response is expected 
    | expected_response                                                                         | require_approve | require_verify_pwd | verify_whole_response |
    | "{"ResponseCode" : "3", "Message" : "COMMON_INTERFACE_ERROR_JSON_DESERIALIZE_EXCEPTION"}" | true            | false              | 0                     |

@Discover
Scenario: test033_OrderID_036_"StopDiscover"_ble module stop scan device interface_Invalid Json Object
	When Discoverui Input Command And Press Send
	| command_one   | command_two    |
	| StartDiscover | {"Status":"0"} |	
	When Discoverui Input Command And Press Send
	| command_one  | command_two |
	| StopDiscover | null        |
	Then The response is expected 
    | expected_response                                                                     | require_approve | require_verify_pwd | verify_whole_response |
    | "{"ResponseCode" : "4", "Message" : "COMMON_INTERFACE_ERROR_JSON_DESERIALIZE_ERROR"}" | true            | false              | 1                     |

@Discover
Scenario: test034_OrderID_037_"StopDiscover"_ble module stop scan device interface_Input Nothing
	When Discoverui Input Command And Press Send
	| command_one  | command_two |
	| StopDiscover |             |
	Then The response is expected 
    | expected_response                                                             | require_approve | require_verify_pwd | verify_whole_response |
    | "{"ResponseCode" : "2", "Message" : "COMMON_INTERFACE_ERROR_INPUT_IS_EMPTY"}" | true            | false              | 1                     |

@Discover
Scenario: test035_OrderID_038_"StopDiscover"_ble module stop scan device interface_The value of key "Status" is wrong
	When Discoverui Input Command And Press Send
	| command_one  | command_two    |
	| StopDiscover | {"Status":"1"} |
	Then The response is expected 
    | expected_response                                                                        | require_approve | require_verify_pwd | verify_whole_response |
    | "{"ResponseCode" : "6", "Message" : "COMMON_INTERFACE_ERROR_JSON_INVALID_STATUS_VALUE"}" | true            | false              | 1                     |

@Discover
Scenario: test036_OrderID_039_"StopDiscover"_ble module stop scan device interface_Missing key "Status"
	When Discoverui Input Command And Press Send
	| command_one  | command_two  |
	| StopDiscover | {"Stus":"0"} |
	Then The response is expected 
    | expected_response                                                               | require_approve | require_verify_pwd | verify_whole_response |
    | "{"ResponseCode" : "5", "Message" : "COMMON_INTERFACE_ERROR_JSON_STATUS_NULL"}" | true            | false              | 1                     |

@Discover
Scenario: test037_OrderID_040_"StopDiscover"_ble module stop scan device interface_The value's type of key "Status" is wrong
	When Discoverui Input Command And Press Send
	| command_one  | command_two  |
	| StopDiscover | {"Status":0} |
	Then The response is expected 
    | expected_response                                                                         | require_approve | require_verify_pwd | verify_whole_response |
    | "{"ResponseCode" : "3", "Message" : "COMMON_INTERFACE_ERROR_JSON_DESERIALIZE_EXCEPTION"}" | true            | false              | 1                     |

@Discover
Scenario: test038_OrderID_041_"StopDiscover"_ble module stop scan device interface_Empty Json Object
	When Discoverui Input Command And Press Send
	| command_one  | command_two |
	| StopDiscover | {}          |
	Then The response is expected 
    | expected_response                                                               | require_approve | require_verify_pwd | verify_whole_response |
    | "{"ResponseCode" : "5", "Message" : "COMMON_INTERFACE_ERROR_JSON_STATUS_NULL"}" | true            | false              | 1                     |

@Discover
Scenario: test039_OrderID_042_"Destroy"_ble module uninitialize interface_Invalid Json Object
    When Discoverui Input Command And Press Send
	| command_one  | command_two    |
	| StopDiscover | {"Status":"0"} |
	When Discoverui Input Command And Press Send 
	| command_one | command_two |
	| Destroy     | null        |
	Then The response is expected 
    | expected_response                                                                     | require_approve | require_verify_pwd | verify_whole_response |
    | "{"ResponseCode" : "4", "Message" : "COMMON_INTERFACE_ERROR_JSON_DESERIALIZE_ERROR"}" | true            | false              | 1                     |

@Discover
Scenario: test040_OrderID_043_"Destroy"_ble module uninitialize interface_Input Nothing
    When Discoverui Input Command And Press Send 
	| command_one | command_two |
	| Destroy     |             |
	Then The response is expected 
    | expected_response                                                             | require_approve | require_verify_pwd | verify_whole_response |
    | "{"ResponseCode" : "2", "Message" : "COMMON_INTERFACE_ERROR_INPUT_IS_EMPTY"}" | true            | false              | 1                     |

@Discover
Scenario: test041_OrderID_044_"Destroy"_ble module uninitialize interface_The value of key "Status" is wrong
    When Discoverui Input Command And Press Send 
	| command_one | command_two    |
	| Destroy     | {"Status":"1"} |
	Then The response is expected 
    | expected_response                                                                        | require_approve | require_verify_pwd | verify_whole_response |
    | "{"ResponseCode" : "6", "Message" : "COMMON_INTERFACE_ERROR_JSON_INVALID_STATUS_VALUE"}" | true            | false              | 1                     |

@Discover
Scenario: test042_OrderID_045_"Destroy"_ble module uninitialize interface_Missing key "Status"
    When Discoverui Input Command And Press Send 
	| command_one | command_two  |
	| Destroy     | {"Stus":"0"} |
	Then The response is expected 
    | expected_response                                                               | require_approve | require_verify_pwd | verify_whole_response |
    | "{"ResponseCode" : "5", "Message" : "COMMON_INTERFACE_ERROR_JSON_STATUS_NULL"}" | true            | false              | 1                     |

@Discover
Scenario: test043_OrderID_046_"Destroy"_ble module uninitialize interface_The value's type of key "Status" is wrong
    When Discoverui Input Command And Press Send 
	| command_one | command_two  |
	| Destroy     | {"Status":0} |
	Then The response is expected 
    | expected_response                                                                         | require_approve | require_verify_pwd | verify_whole_response |
    | "{"ResponseCode" : "3", "Message" : "COMMON_INTERFACE_ERROR_JSON_DESERIALIZE_EXCEPTION"}" | true            | false              | 1                     |

@Discover
Scenario: test044_OrderID_047_"Destroy"_ble module uninitialize interface_Empty Json Object
    When Discoverui Input Command And Press Send 
	| command_one | command_two    |
	| Destroy     | {} |
	Then The response is expected 
    | expected_response                                                               | require_approve | require_verify_pwd | verify_whole_response |
    | "{"ResponseCode" : "5", "Message" : "COMMON_INTERFACE_ERROR_JSON_STATUS_NULL"}" | true            | false              | 1                     |	

@Discover
Scenario: test0441_Close all windows
    When Discoverui Input Command And Press Send
	| command_one | command_two    |
	| Stop        | {"Status":"0"} |
	When Click Stop Button on The window_name
	| window_name |
	| discover    |
	When close connect or discover UI
	| window_name |
	| discover    |
 	When Click Stop Button on The window_name
	| window_name |
	| connect     |
    When close connect or discover UI
	| window_name |
	| connect     |
	When close connect or discover UI
	| window_name |
	| unity       |

@Discover
Scenario Template: test0442_open_ldpp_exe
	Given Open Ldpp App Window <exe_path>
	And Add Waiting Time <sleep_time1>
	When Click Sign In Button
Scenarios:
    | exe_path                                                        | sleep_time1 |
    | C:\\DCLDPP\\net6.0-windows10.0.19041\\UnitySampleAppWindows.exe | 500         |

@Discover
Scenario Template: test0443_open_window_and_click_start_button
	When Choose <window_name> from The Following Windows Options And Click Test Button
	When Click Start Button on The <window_name> Window
	Then The response is expected <expected_response> in <require_approve> under <require_verify_pwd> after <verify_whole_response> 
Scenarios:
    | window_name | expected_response                         | require_approve | require_verify_pwd | verify_whole_response |
    | connect     | "plugin \| Plugin started successfully!!" | true            | false              | 1                     |

@Discover
Scenario Template: test0444_open_window_and_click_start_button
	When Click Start Button on The <window_name> Window
	Then The response is expected <expected_response> in <require_approve> under <require_verify_pwd> after <verify_whole_response> 
Scenarios:
    | window_name | expected_response                         | require_approve | require_verify_pwd | verify_whole_response |
    | discover    | "plugin \| Plugin started successfully!!" | true            | false              | 1                     |

@Discover
Scenario: test045_OrderID_048_"ReadyForLink"_ble module connect to peer device interface_Missing key "Device"  
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
	When Discoverui Input Command And Press Send
	| command_one  | command_two        |
	| ReadyForLink | MissingDeviceError |
	Then The response is expected 
    | expected_response                                                             | require_approve | require_verify_pwd | verify_whole_response |
    | "{"ResponseCode" : "5", "Message" : "PREPARE_LINK_JSON_NOTIFY_DEVICE_EMPTY"}" | true            | false              | 1                     |

@Discover
Scenario: test046_OrderID_049_"ReadyForLink"_ble module connect to peer device interface_Missing key "Name"    
	When Discoverui Input Command And Press Send
	| command_one  | command_two    |
	| StopDiscover | {"Status":"0"} |
	When connectui Input Command And Press Send
	| command_one | command_two    |
	| CloseAP     | {"Status":"0"} |
	When connectui Input Command And Press Send 
	| command_one | command_two |
	| CreateAP    | randomPwd   |
	When Discoverui Input Command And Press Send
	| command_one   | command_two    |
	| StartDiscover | {"Status":"0"} |
	When Discoverui Input Command And Press Send
	| command_one  | command_two      |
	| ReadyForLink | MissingNameError |
	Then The response is expected 
    | expected_response                                                                                  | require_approve | require_verify_pwd | verify_whole_response |
    | "{"ResponseCode" : "0", "Message" : "PREPARE_LINK_OK", "Payload":"{"Status": 0 , "Message": ""}"}" | true            | false              | 1                     |

@Discover
Scenario: test047_OrderID_050_"ReadyForLink"_ble module connect to peer device interface_Invalid value of key "Name"    
	When Discoverui Input Command And Press Send
	| command_one  | command_two    |
	| StopDiscover | {"Status":"0"} |
	When connectui Input Command And Press Send
	| command_one | command_two    |
	| CloseAP     | {"Status":"0"} |
	When connectui Input Command And Press Send 
	| command_one | command_two |
	| CreateAP    | randomPwd   |
	When Discoverui Input Command And Press Send
	| command_one   | command_two    |
	| StartDiscover | {"Status":"0"} |
	When Discoverui Input Command And Press Send
	| command_one  | command_two      |
	| ReadyForLink | InvalidNameError |
	Then The response is expected 
    | expected_response                                                                                  | require_approve | require_verify_pwd | verify_whole_response |
    | "{"ResponseCode" : "0", "Message" : "PREPARE_LINK_OK", "Payload":"{"Status": 0 , "Message": ""}"}" | true            | false              | 1                     |

@Discover
Scenario: test048_OrderID_051_"ReadyForLink"_ble module connect to peer device interface_Missing key "BLE"
	When Discoverui Input Command And Press Send
	| command_one  | command_two    |
	| StopDiscover | {"Status":"0"} |
	When connectui Input Command And Press Send
	| command_one | command_two    |
	| CloseAP     | {"Status":"0"} |
	When connectui Input Command And Press Send 
	| command_one | command_two |
	| CreateAP    | randomPwd   |
	When Discoverui Input Command And Press Send
	| command_one   | command_two    |
	| StartDiscover | {"Status":"0"} |
	When Discoverui Input Command And Press Send
	| command_one  | command_two     |
	| ReadyForLink | MissingBLEError |
	Then The response is expected 
    | expected_response                                                          | require_approve | require_verify_pwd | verify_whole_response |
    | "{"ResponseCode" : "8", "Message" : "PREPARE_LINK_JSON_NOTIFY_BLE_EMPTY"}" | true            | false              | 1                     |

@Discover
Scenario: test049_OrderID_052_"ReadyForLink"_ble module connect to peer device interface_Invalid value of key "BLE"
	When Discoverui Input Command And Press Send
	| command_one  | command_two    |
	| StopDiscover | {"Status":"0"} |
	When connectui Input Command And Press Send
	| command_one | command_two    |
	| CloseAP     | {"Status":"0"} |
	When connectui Input Command And Press Send 
	| command_one | command_two |
	| CreateAP    | randomPwd   |
	When Discoverui Input Command And Press Send
	| command_one   | command_two    |
	| StartDiscover | {"Status":"0"} |
	When Discoverui Input Command And Press Send
	| command_one  | command_two    |
	| ReadyForLink | InvalidBLError |
	Then The response is expected 
    | expected_response                                                                                  | require_approve | require_verify_pwd | verify_whole_response |
    | "{"ResponseCode" : "0", "Message" : "PREPARE_LINK_OK", "Payload":"{"Status": 0 , "Message": ""}"}" | true            | false              | 1                     |

@Discover
Scenario: test050_OrderID_053_"ReadyForLink"_ble module connect to peer device interface_Invalid value of key "BT"
	When Discoverui Input Command And Press Send
	| command_one  | command_two    |
	| StopDiscover | {"Status":"0"} |
	When connectui Input Command And Press Send
	| command_one | command_two    |
	| CloseAP     | {"Status":"0"} |
	When connectui Input Command And Press Send 
	| command_one | command_two |
	| CreateAP    | randomPwd   |
	When Discoverui Input Command And Press Send
	| command_one   | command_two    |
	| StartDiscover | {"Status":"0"} |
	When Discoverui Input Command And Press Send
	| command_one  | command_two    |
	| ReadyForLink | InvalidBTError |
	Then The response is expected 
    | expected_response                                                                                  | require_approve | require_verify_pwd | verify_whole_response |
    | "{"ResponseCode" : "0", "Message" : "PREPARE_LINK_OK", "Payload":"{"Status": 0 , "Message": ""}"}" | true            | false              | 1                     |

@Discover
Scenario: test051_OrderID_054_"ReadyForLink"_ble module connect to peer device interface_Empty Json Object
	When Discoverui Input Command And Press Send
	| command_one  | command_two |
	| StopDiscover | {}          |
	When connectui Input Command And Press Send
	| command_one | command_two    |
	| CloseAP     | {"Status":"0"} |
	When connectui Input Command And Press Send 
	| command_one | command_two |
	| CreateAP    | randomPwd   |
	When Discoverui Input Command And Press Send
	| command_one   | command_two    |
	| StartDiscover | {"Status":"0"} |
	When Discoverui Input Command And Press Send
	| command_one  | command_two |
	| ReadyForLink | {}          |
	Then The response is expected 
    | expected_response                                                             | require_approve | require_verify_pwd | verify_whole_response |
    | "{"ResponseCode" : "5", "Message" : "PREPARE_LINK_JSON_NOTIFY_DEVICE_EMPTY"}" | true            | false              | 1                     |

@Discover
Scenario: test052_OrderID_055_"ReadyForLink"_ble module connect to peer device interface_Invalid Json Object
	When Discoverui Input Command And Press Send
	| command_one  | command_two    |
	| StopDiscover | {"Status":"0"} |
	When connectui Input Command And Press Send
	| command_one | command_two    |
	| CloseAP     | {"Status":"0"} |
	When connectui Input Command And Press Send 
	| command_one | command_two |
	| CreateAP    | randomPwd   |
	When Discoverui Input Command And Press Send
	| command_one   | command_two    |
	| StartDiscover | {"Status":"0"} |
	When Discoverui Input Command And Press Send
	| command_one  | command_two |
	| ReadyForLink | null        |
	Then The response is expected 
    | expected_response                                                           | require_approve | require_verify_pwd | verify_whole_response |
    | "{"ResponseCode" : "4", "Message" : "PREPARE_LINK_JSON_DESERIALIZE_ERROR"}" | true            | false              | 1                     |

@Discover
Scenario: test053_OrderID_056_"ReadyForLink"_ble module connect to peer device interface_Input Nothing
	When Discoverui Input Command And Press Send
	| command_one  | command_two    |
	| StopDiscover | {"Status":"0"} |
	When connectui Input Command And Press Send
	| command_one | command_two    |
	| CloseAP     | {"Status":"0"} |
	When connectui Input Command And Press Send 
	| command_one | command_two |
	| CreateAP    | randomPwd   |
	When Discoverui Input Command And Press Send
	| command_one   | command_two    |
	| StartDiscover | {"Status":"0"} |
	When Discoverui Input Command And Press Send
	| command_one  | command_two |
	| ReadyForLink |             |
	Then The response is expected 
    | expected_response                                                   | require_approve | require_verify_pwd | verify_whole_response |
    | "{"ResponseCode" : "2", "Message" : "PREPARE_LINK_INPUT_IS_EMPTY"}" | true            | false              | 1                     |

@Discover
Scenario: test054_OrderID_057_"SetWifiApInfo"_ble modue SetWifiApInfo interface_Missing key "SSID"
	When Discoverui Input Command And Press Send
	| command_one  | command_two    |
	| StopDiscover | {"Status":"0"} |
	When connectui Input Command And Press Send
	| command_one | command_two    |
	| CloseAP     | {"Status":"0"} |
	When connectui Input Command And Press Send 
	| command_one | command_two |
	| CreateAP    | randomPwd   |
	When Discoverui Input Command And Press Send
	| command_one   | command_two         |
	| SetWifiApInfo | MissingkeySSIDError |
	Then The response is expected 
    | expected_response                                                     | require_approve | require_verify_pwd | verify_whole_response |
    | "{"ResponseCode" : "5", "Message" : "SET_WIFI_INFO_JSON_SSID_EMPTY"}" | true            | false              | 1                     |

@Discover
Scenario: test055_OrderID_058_"SetWifiApInfo"_ble modue SetWifiApInfo interface_Invalid value of key "PWD"
	When connectui Input Command And Press Send
	| command_one | command_two    |
	| CloseAP     | {"Status":"0"} |
	When connectui Input Command And Press Send 
	| command_one | command_two |
	| CreateAP    | randomPwd   |
	When Discoverui Input Command And Press Send
	| command_one   | command_two               |
	| SetWifiApInfo | InvalidvalueofkeyPWDError |
	Then The response is expected 
    | expected_response                                                                | require_approve | require_verify_pwd | verify_whole_response |
    | "{"ResponseCode" : "3", "Message" : "SET_WIFI_INFO_JSON_DESERIALIZE_EXCEPTION"}" | true            | false              | 1                     |

@Discover
Scenario: test056_OrderID_059_"SetWifiApInfo"_ble modue SetWifiApInfo interface_Empty Json Object
	When connectui Input Command And Press Send
	| command_one | command_two    |
	| CloseAP     | {"Status":"0"} |
	When connectui Input Command And Press Send 
	| command_one | command_two |
	| CreateAP    | randomPwd   |
	When Discoverui Input Command And Press Send
	| command_one   | command_two |
	| SetWifiApInfo | {}          |
	Then The response is expected 
    | expected_response                                                     | require_approve | require_verify_pwd | verify_whole_response |
    | "{"ResponseCode" : "5", "Message" : "SET_WIFI_INFO_JSON_SSID_EMPTY"}" | true            | false              | 1                     |

@Discover
Scenario: test057_OrderID_060_"SetWifiApInfo"_ble modue SetWifiApInfo interface_Input Nothing
	When connectui Input Command And Press Send
	| command_one | command_two    |
	| CloseAP     | {"Status":"0"} |
	When connectui Input Command And Press Send 
	| command_one | command_two |
	| CreateAP    | randomPwd   |
	When Discoverui Input Command And Press Send
	| command_one   | command_two |
	| SetWifiApInfo |             |
	Then The response is expected 
    | expected_response                                                    | require_approve | require_verify_pwd | verify_whole_response |
    | "{"ResponseCode" : "2", "Message" : "SET_WIFI_INFO_INPUT_IS_EMPTY"}" | true            | false              | 1                     |

@Discover
Scenario: test058_OrderID_061_"SetWifiApInfo"_ble modue SetWifiApInfo interface_Invalid Json Object
	When connectui Input Command And Press Send
	| command_one | command_two    |
	| CloseAP     | {"Status":"0"} |
	When connectui Input Command And Press Send 
	| command_one | command_two |
	| CreateAP    | randomPwd   |
	When Discoverui Input Command And Press Send
	| command_one   | command_two |
	| SetWifiApInfo | null        |
	Then The response is expected 
    | expected_response                                                            | require_approve | require_verify_pwd | verify_whole_response |
    | "{"ResponseCode" : "4", "Message" : "SET_WIFI_INFO_JSON_DESERIALIZE_ERROR"}" | true            | false              | 1                     |

@Discover
Scenario: test059_OrderID_062_"SetWifiApInfo"_ble modue SetWifiApInfo interface_Invalid key "PWD"
	When connectui Input Command And Press Send
	| command_one | command_two    |
	| CloseAP     | {"Status":"0"} |
	When connectui Input Command And Press Send 
	| command_one | command_two |
	| CreateAP    | randomPwd   |
	When Discoverui Input Command And Press Send
	| command_one   | command_two   |
	| SetWifiApInfo | InvalidkeyPWD |
	Then The response is expected 
    | expected_response                                                                | require_approve | require_verify_pwd | verify_whole_response |
    | "{"ResponseCode" : "3", "Message" : "SET_WIFI_INFO_JSON_DESERIALIZE_EXCEPTION"}" | true            | false              | 1                     |

@Discover
Scenario: test060_OrderID_063_"SetWifiApInfo"_ble modue SetWifiApInfo interface_The length of value of key "PWD" is shorter than 8
	When connectui Input Command And Press Send
	| command_one | command_two    |
	| CloseAP     | {"Status":"0"} |
	When connectui Input Command And Press Send 
	| command_one | command_two |
	| CreateAP    | randomPwd   |
	When Discoverui Input Command And Press Send
	| command_one   | command_two          |
	| SetWifiApInfo | PWDshorterthan8Error |
	Then The response is expected 
    | expected_response                                                            | require_approve | require_verify_pwd | verify_whole_response |
    | "{"ResponseCode" : "7", "Message" : "SET_WIFI_INFO_JSON_LENGTH_IS_ILLEGAL"}" | true            | false              | 1                     |

@Discover
Scenario: test061_OrderID_064_"SetWifiApInfo"_ble modue SetWifiApInfo interface_The length of value of key "PWD" is longer than 32
	When connectui Input Command And Press Send
	| command_one | command_two    |
	| CloseAP     | {"Status":"0"} |
	When connectui Input Command And Press Send 
	| command_one | command_two |
	| CreateAP    | randomPwd   |
	When Discoverui Input Command And Press Send
	| command_one   | command_two          |
	| SetWifiApInfo | PWDlongerthan32Error |
	Then The response is expected 
    | expected_response                                                            | require_approve | require_verify_pwd | verify_whole_response |
    | "{"ResponseCode" : "7", "Message" : "SET_WIFI_INFO_JSON_LENGTH_IS_ILLEGAL"}" | true            | false              | 1                     |

@Discover
Scenario: test062_OrderID_065_"StartFind"_ble module ready to send ble broadcast_Invalid Json Object
	When connectui Input Command And Press Send
	| command_one | command_two    |
	| CloseAP     | {"Status":"0"} |
	When connectui Input Command And Press Send 
	| command_one | command_two |
	| CreateAP    | randomPwd   |
	When Discoverui Input Command And Press Send
	| command_one   | command_two         |
	| SetWifiApInfo | SetWifiApInfoNormal |
	When Discoverui Input Command And Press Send
	| command_one | command_two |
	| StartFind   | null        |
	Then The response is expected 
    | expected_response                                                                     | require_approve | require_verify_pwd | verify_whole_response |
    | "{"ResponseCode" : "4", "Message" : "COMMON_INTERFACE_ERROR_JSON_DESERIALIZE_ERROR"}" | true            | false              | 1                     |

@Discover
Scenario: test063_OrderID_066_"StartFind"_ble module ready to send ble broadcast_Input Nothing
	When connectui Input Command And Press Send
	| command_one | command_two    |
	| CloseAP     | {"Status":"0"} |
	When connectui Input Command And Press Send 
	| command_one | command_two |
	| CreateAP    | randomPwd   |
	When Discoverui Input Command And Press Send
	| command_one   | command_two         |
	| SetWifiApInfo | SetWifiApInfoNormal |
	When Discoverui Input Command And Press Send
	| command_one | command_two |
	| StartFind   |             |
	Then The response is expected 
    | expected_response                                                             | require_approve | require_verify_pwd | verify_whole_response |
    | "{"ResponseCode" : "2", "Message" : "COMMON_INTERFACE_ERROR_INPUT_IS_EMPTY"}" | true            | false              | 1                     |

@Discover	
Scenario: test064_OrderID_067_"StartFind"_ble module ready to send ble broadcast_The value of key "Status" is wrong
	When connectui Input Command And Press Send
	| command_one | command_two    |
	| CloseAP     | {"Status":"0"} |
	When connectui Input Command And Press Send 
	| command_one | command_two |
	| CreateAP    | randomPwd   |
	When Discoverui Input Command And Press Send
	| command_one   | command_two         |
	| SetWifiApInfo | SetWifiApInfoNormal |
	When Discoverui Input Command And Press Send
	| command_one | command_two    |
	| StartFind   | {"Status":"1"} |
	Then The response is expected 
    | expected_response                                                                        | require_approve | require_verify_pwd | verify_whole_response |
    | "{"ResponseCode" : "6", "Message" : "COMMON_INTERFACE_ERROR_JSON_INVALID_STATUS_VALUE"}" | true            | false              | 1                     |

@Discover
Scenario: test065_OrderID_068_"StartFind"_ble module ready to send ble broadcast_Missing key "Status"
	When connectui Input Command And Press Send
	| command_one | command_two    |
	| CloseAP     | {"Status":"0"} |
	When connectui Input Command And Press Send 
	| command_one | command_two |
	| CreateAP    | randomPwd   |
	When Discoverui Input Command And Press Send
	| command_one   | command_two         |
	| SetWifiApInfo | SetWifiApInfoNormal |
	When Discoverui Input Command And Press Send
	| command_one | command_two  |
	| StartFind   | {"Stus":"0"} |
	Then The response is expected 
    | expected_response                                                               | require_approve | require_verify_pwd | verify_whole_response |
    | "{"ResponseCode" : "5", "Message" : "COMMON_INTERFACE_ERROR_JSON_STATUS_NULL"}" | true            | false              | 1                     |

@Discover
Scenario: test066_OrderID_069_"StartFind"_ble module ready to send ble broadcast_The value's type of key "Status" is wrong
	When connectui Input Command And Press Send
	| command_one | command_two    |
	| CloseAP     | {"Status":"0"} |
	When connectui Input Command And Press Send 
	| command_one | command_two |
	| CreateAP    | randomPwd   |
	When Discoverui Input Command And Press Send
	| command_one   | command_two         |
	| SetWifiApInfo | SetWifiApInfoNormal |
	When Discoverui Input Command And Press Send
	| command_one | command_two  |
	| StartFind   | {"Status":0} |
	Then The response is expected 
    | expected_response                                                                         | require_approve | require_verify_pwd | verify_whole_response |
    | "{"ResponseCode" : "3", "Message" : "COMMON_INTERFACE_ERROR_JSON_DESERIALIZE_EXCEPTION"}" | true            | false              | 1                     |

@Discover
Scenario: test067_OrderID_070_"StartFind"_ble module ready to send ble broadcast_Empty Json Object
	When connectui Input Command And Press Send
	| command_one | command_two    |
	| CloseAP     | {"Status":"0"} |
	When connectui Input Command And Press Send 
	| command_one | command_two |
	| CreateAP    | randomPwd   |
	When Discoverui Input Command And Press Send
	| command_one   | command_two         |
	| SetWifiApInfo | SetWifiApInfoNormal |
	When Discoverui Input Command And Press Send
	| command_one | command_two |
	| StartFind   | {}          |
	Then The response is expected 
    | expected_response                                                               | require_approve | require_verify_pwd | verify_whole_response |
    | "{"ResponseCode" : "5", "Message" : "COMMON_INTERFACE_ERROR_JSON_STATUS_NULL"}" | true            | false              | 1                     |

@Discover
Scenario: test068_OrderID_071_"StopFind"_ble module ready to send ble broadcast_Invalid Json Object
	When connectui Input Command And Press Send
	| command_one | command_two    |
	| CloseAP     | {"Status":"0"} |
	When connectui Input Command And Press Send 
	| command_one | command_two |
	| CreateAP    | randomPwd   |
	When Discoverui Input Command And Press Send
	| command_one   | command_two         |
	| SetWifiApInfo | SetWifiApInfoNormal |
	When Discoverui Input Command And Press Send
	| command_one | command_two    |
	| StartFind   | {"Status":"0"} |
	When Discoverui Input Command And Press Send
	| command_one | command_two |
	| StopFind    | null        |
	Then The response is expected 
    | expected_response                                                                     | require_approve | require_verify_pwd | verify_whole_response |
    | "{"ResponseCode" : "4", "Message" : "COMMON_INTERFACE_ERROR_JSON_DESERIALIZE_ERROR"}" | true            | false              | 1                     |

@Discover
Scenario: test069_OrderID_072_"StopFind"_ble module ready to send ble broadcast_Input Nothing
	When Discoverui Input Command And Press Send
	| command_one | command_two |
	| StopFind    |             |
	Then The response is expected 
    | expected_response                                                             | require_approve | require_verify_pwd | verify_whole_response |
    | "{"ResponseCode" : "2", "Message" : "COMMON_INTERFACE_ERROR_INPUT_IS_EMPTY"}" | true            | false              | 1                     |

@Discover
Scenario: test070_OrderID_073_"StopFind"_ble module ready to send ble broadcast_The value of key "Status" is wrong
	When Discoverui Input Command And Press Send
	| command_one | command_two    |
	| StopFind    | {"Status":"1"} |
	Then The response is expected 
    | expected_response                                                                        | require_approve | require_verify_pwd | verify_whole_response |
    | "{"ResponseCode" : "6", "Message" : "COMMON_INTERFACE_ERROR_JSON_INVALID_STATUS_VALUE"}" | true            | false              | 1                     |

@Discover
Scenario: test071_OrderID_074_"StopFind"_ble module ready to send ble broadcast_Missing key "Status"
	When Discoverui Input Command And Press Send
	| command_one | command_two  |
	| StopFind    | {"Stus":"0"} |
	Then The response is expected 
    | expected_response                                                               | require_approve | require_verify_pwd | verify_whole_response |
    | "{"ResponseCode" : "5", "Message" : "COMMON_INTERFACE_ERROR_JSON_STATUS_NULL"}" | true            | false              | 1                     |

@Discover
Scenario: test072_OrderID_075_"StopFind"_ble module ready to send ble broadcast_The value's type of key "Status" is wrong
	When Discoverui Input Command And Press Send
	| command_one | command_two  |
	| StopFind    | {"Status":0} |
	Then The response is expected 
    | expected_response                                                                         | require_approve | require_verify_pwd | verify_whole_response |
    | "{"ResponseCode" : "3", "Message" : "COMMON_INTERFACE_ERROR_JSON_DESERIALIZE_EXCEPTION"}" | true            | false              | 1                     |

@Discover
Scenario: test073_OrderID_076_"StopFind"_ble module ready to send ble broadcast_Empty Json Object
	When Discoverui Input Command And Press Send
	| command_one | command_two |
	| StopFind    | {}          |
	Then The response is expected 
    | expected_response                                                               | require_approve | require_verify_pwd | verify_whole_response |
    | "{"ResponseCode" : "5", "Message" : "COMMON_INTERFACE_ERROR_JSON_STATUS_NULL"}" | true            | false              | 1                     |

@Discover
Scenario: test074_Stop
	When Discoverui Input Command And Press Send
	| command_one | command_two    |
	| StopFind    | {"Status":"0"} |
	When connectui Input Command And Press Send
	| command_one | command_two    |
	| CloseAP     | {"Status":"0"} |
	When Discoverui Input Command And Press Send
	| command_one | command_two    |
	| Stop        | {"Status":"0"} |
	When connectui Input Command And Press Send
	| command_one | command_two    |
	| Stop        | {"Status":"0"} |

@Discover
#connect ui stop button
Scenario: test075_close_connect_button
 	When Click Stop Button on The window_name
	| window_name |
	| connect     |

@Discover
#discover ui stop button
Scenario: test076_close_discover_button
	When Click Stop Button on The window_name
	| window_name |
	| discover    |

@Discover
#关闭connect窗口
Scenario: test077_close_connect
	When close connect or discover UI
	| window_name |
	| connect     |
#关闭discover窗口

@Discover
Scenario: test078_close_discover
	When close connect or discover UI
	| window_name |
	| discover    |

@Discover
#关闭主窗口
Scenario: test079_close_unity
	When close connect or discover UI
	| window_name |
	| unity       |