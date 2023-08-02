Feature: Discover_Dependency_Repeatability


A short summary of the feature
Background: 
@Discover
Scenario Template: test0_open_ldpp_exe_discover_dependency
	Given Open Ldpp App Window <exe_path>
	And Add Waiting Time <sleep_time1>
	When Click Sign In Button
Scenarios:
    | exe_path                                                        | sleep_time1 |
    | C:\\DCLDPP\\net6.0-windows10.0.19041\\UnitySampleAppWindows.exe | 500         |

@Discover
# open main test button,and open connect and discover UI
Scenario Template: test001_open_window_and_test_discover_dependency
	When Choose <window_name> from The Following Windows Options And Click Test Button 
Scenarios:
    | window_name |
    |             |

@Discover
Scenario Template: test002_open_window_and_click_start_button_discover_dependency
	When Click Start Button on The <window_name> Window
	Then The response is expected <expected_response> in <require_approve> under <require_verify_pwd> after <verify_whole_response> 
Scenarios:
    | window_name | expected_response                         | require_approve | require_verify_pwd | verify_whole_response |
    | connect     | "plugin \| Plugin started successfully!!" | true            | false              | 1                     |

@Discover
Scenario Template: test003_open_window_and_click_start_button_discover_dependency
	When Click Start Button on The <window_name> Window
	Then The response is expected <expected_response> in <require_approve> under <require_verify_pwd> after <verify_whole_response> 
Scenarios:
    | window_name | expected_response                         | require_approve | require_verify_pwd | verify_whole_response |
    | discover    | "plugin \| Plugin started successfully!!" | true            | false              | 1                     |

#接口说明
#connectui Input Command And Press Send ：connect ui 输入命令
#Discoverui Input Command And Press Send：Discover ui 输入指令

@Discover
Scenario: test004_OrderID_077_"Start"_Interface dependency testing
	When Discoverui Input Command And Press Send
	| command_one | command_two    |
	| Stop        | {"Status":"0"} |
	Then The response is expected 
    | expected_response                                             | require_approve | require_verify_pwd | verify_whole_response |
    | "{"ResponseCode" : "5", "Message" : "BLE_SMS_ERROR_UNSTART"}" | true            | false              | 1                     |

@Discover
Scenario: test005_OrderID_078_"Start"_Interface dependency testing
	When Discoverui Input Command And Press Send
	| command_one   | command_two    |
	| StartDiscover | {"Status":"0"} |
	Then The response is expected 
    | expected_response                                             | require_approve | require_verify_pwd | verify_whole_response |
    | "{"ResponseCode" : "5", "Message" : "BLE_SMS_ERROR_UNSTART"}" | true            | false              | 1                     |

@Discover
Scenario: test006_OrderID_079_"Start"_Interface dependency testing
	When Discoverui Input Command And Press Send
	| command_one  | command_two    |
	| StopDiscover | {"Status":"0"} |
	Then The response is expected 
    | expected_response                                                      | require_approve | require_verify_pwd | verify_whole_response |
    | "{"ResponseCode" : "6", "Message" : "BLE_SMS_ERROR_UNSTART_DISCOVER"}" | true            | false              | 1                     |

@Discover
Scenario: test007_OrderID_080_"Start"_Interface dependency testing
	When Discoverui Input Command And Press Send
	| command_one  | command_two |
	| ReadyForLink | {}          |
	Then The response is expected 
    | expected_response                                                      | require_approve | require_verify_pwd | verify_whole_response |
    | "{"ResponseCode" : "6", "Message" : "BLE_SMS_ERROR_UNSTART_DISCOVER"}" | true            | false              | 1                     |

@Discover
Scenario: test008_OrderID_081_"Start"_Interface dependency testing
	When Discoverui Input Command And Press Send
	| command_one   | command_two |
	| SetWifiApInfo | {}          |
	Then The response is expected 
    | expected_response                                             | require_approve | require_verify_pwd | verify_whole_response |
    | "{"ResponseCode" : "5", "Message" : "BLE_SMS_ERROR_UNSTART"}" | true            | false              | 1                     |

@Discover
Scenario: test009_OrderID_082_"Start"_Interface dependency testing
	When Discoverui Input Command And Press Send
	| command_one | command_two    |
	| StartFind   | {"Status":"0"} |
	Then The response is expected 
    | expected_response                                                     | require_approve | require_verify_pwd | verify_whole_response |
    | "{"ResponseCode" : "10", "Message" : "BLE_SMS_ERROR_UN_SET_WIFI_AP"}" | true            | false              | 1                     |

@Discover
Scenario: test010_OrderID_083_"Start"_Interface dependency testing
	When Discoverui Input Command And Press Send
	| command_one | command_two    |
	| StopFind    | {"Status":"0"} |
	Then The response is expected 
    | expected_response                                                       | require_approve | require_verify_pwd | verify_whole_response |
    | "{"ResponseCode" : "8", "Message" : "BLE_SMS_ERROR_UN_START_BE_FOUND"}" | true            | false              | 1                     |

@Discover
Scenario: test011_OrderID_084_"Stop"_Interface dependency testing
    When Discoverui Input Command And Press Send
	| command_one | command_two            |
	| Start       | {"IsHomeGroup":"TRUE"} |
	When Discoverui Input Command And Press Send
	| command_one | command_two    |
	| Stop        | {"Status":"0"} |
	When Discoverui Input Command And Press Send
	| command_one   | command_two    |
	| StartDiscover | {"Status":"0"} |
	Then The response is expected 
    | expected_response                                             | require_approve | require_verify_pwd | verify_whole_response |
    | "{"ResponseCode" : "5", "Message" : "BLE_SMS_ERROR_UNSTART"}" | true            | false              | 1                     |

@Discover
Scenario: test012_OrderID_085_"Stop"_Interface dependency testing
	When Discoverui Input Command And Press Send
	| command_one  | command_two    |
	| StopDiscover | {"Status":"0"} |
	Then The response is expected 
    | expected_response                                                      | require_approve | require_verify_pwd | verify_whole_response |
    | "{"ResponseCode" : "6", "Message" : "BLE_SMS_ERROR_UNSTART_DISCOVER"}" | true            | false              | 1                     |

@Discover
Scenario: test013_OrderID_086_"Stop"_Interface dependency testing
	When Discoverui Input Command And Press Send
	| command_one  | command_two |
	| ReadyForLink | {}          |
	Then The response is expected 
    | expected_response                                                      | require_approve | require_verify_pwd | verify_whole_response |
    | "{"ResponseCode" : "6", "Message" : "BLE_SMS_ERROR_UNSTART_DISCOVER"}" | true            | false              | 1                     |

@Discover
Scenario: test014_OrderID_087_"Stop"_Interface dependency testing
	When Discoverui Input Command And Press Send
	| command_one   | command_two |
	| SetWifiApInfo | {}          |
	Then The response is expected 
    | expected_response                                             | require_approve | require_verify_pwd | verify_whole_response |
    | "{"ResponseCode" : "5", "Message" : "BLE_SMS_ERROR_UNSTART"}" | true            | false              | 1                     |

@Discover
Scenario: test015_OrderID_088_"Stop"_Interface dependency testing
	When Discoverui Input Command And Press Send
	| command_one | command_two    |
	| StartFind   | {"Status":"0"} |
	Then The response is expected 
    | expected_response                                                     | require_approve | require_verify_pwd | verify_whole_response |
    | "{"ResponseCode" : "10", "Message" : "BLE_SMS_ERROR_UN_SET_WIFI_AP"}" | true            | false              | 1                     |

@Discover
Scenario: test016_OrderID_089_"Stop"_Interface dependency testing
	When Discoverui Input Command And Press Send
	| command_one | command_two    |
	| StopFind    | {"Status":"0"} |
	Then The response is expected 
    | expected_response                                                       | require_approve | require_verify_pwd | verify_whole_response |
    | "{"ResponseCode" : "8", "Message" : "BLE_SMS_ERROR_UN_START_BE_FOUND"}" | true            | false              | 1                     |

@Discover
Scenario: test017_OrderID_090_"Stop"_Interface dependency testing
	When Discoverui Input Command And Press Send
	| command_one | command_two    |
	| Destroy     | {"Status":"0"} |
	Then The response is expected 
    | expected_response                                             | require_approve | require_verify_pwd | verify_whole_response |
    | "{"ResponseCode" : "5", "Message" : "BLE_SMS_ERROR_UNSTART"}" | true            | false              | 1                     |

@Discover
Scenario: test018_OrderID_091_"StartDiscover"_Interface dependency testing
	When Discoverui Input Command And Press Send
	| command_one | command_two            |
	| Start       | {"IsHomeGroup":"TRUE"} |
	When Discoverui Input Command And Press Send
	| command_one  | command_two    |
	| StopDiscover | {"Status":"0"} |
	Then The response is expected 
    | expected_response                                                      | require_approve | require_verify_pwd | verify_whole_response |
    | "{"ResponseCode" : "6", "Message" : "BLE_SMS_ERROR_UNSTART_DISCOVER"}" | true            | false              | 1                     |

@Discover
Scenario: test019_OrderID_092_"StartDiscover"_Interface dependency testing
	When Discoverui Input Command And Press Send
	| command_one  | command_two |
	| ReadyForLink | {}          |
	Then The response is expected 
    | expected_response                                                      | require_approve | require_verify_pwd | verify_whole_response |
    | "{"ResponseCode" : "6", "Message" : "BLE_SMS_ERROR_UNSTART_DISCOVER"}" | true            | false              | 1                     |

@Discover
Scenario: test020_OrderID_093_"StopDiscover"_Interface dependency testing
	When Discoverui Input Command And Press Send
	| command_one   | command_two    |
	| StartDiscover | {"Status":"0"} |
	When Discoverui Input Command And Press Send
	| command_one  | command_two    |
	| StopDiscover | {"Status":"0"} |
	When Discoverui Input Command And Press Send
	| command_one  | command_two |
	| ReadyForLink | {}          |
	Then The response is expected 
    | expected_response                                                      | require_approve | require_verify_pwd | verify_whole_response |
    | "{"ResponseCode" : "6", "Message" : "BLE_SMS_ERROR_UNSTART_DISCOVER"}" | true            | false              | 1                     |

@Discover
Scenario: test021_OrderID_094_"Destroy"_Interface dependency testing
	When Discoverui Input Command And Press Send
	| command_one | command_two    |
	| Destroy     | {"Status":"0"} |
	When Discoverui Input Command And Press Send
    | command_one | command_two    |
	| Stop        | {"Status":"0"} |
	Then The response is expected 
    | expected_response                                             | require_approve | require_verify_pwd | verify_whole_response |
    | "{"ResponseCode" : "5", "Message" : "BLE_SMS_ERROR_UNSTART"}" | true            | false              | 1                     |

@Discover
Scenario: test022_OrderID_095_"Destroy"_Interface dependency testing
	When Discoverui Input Command And Press Send
    | command_one   | command_two    |
    | StartDiscover | {"Status":"0"} |
	Then The response is expected 
    | expected_response                                             | require_approve | require_verify_pwd | verify_whole_response |
    | "{"ResponseCode" : "5", "Message" : "BLE_SMS_ERROR_UNSTART"}" | true            | false              | 1                     |

@Discover
Scenario: test023_OrderID_096_"Destroy"_Interface dependency testing
	When Discoverui Input Command And Press Send
    | command_one  | command_two    |
    | StopDiscover | {"Status":"0"} |
	Then The response is expected 
    | expected_response                                                      | require_approve | require_verify_pwd | verify_whole_response |
    | "{"ResponseCode" : "6", "Message" : "BLE_SMS_ERROR_UNSTART_DISCOVER"}" | true            | false              | 1                     |

@Discover
Scenario: test024_OrderID_097_"Destroy"_Interface dependency testing
	When Discoverui Input Command And Press Send
    | command_one  | command_two    |
    | ReadyForLink | {"Status":"0"} |
	Then The response is expected 
    | expected_response                                                      | require_approve | require_verify_pwd | verify_whole_response |
    | "{"ResponseCode" : "6", "Message" : "BLE_SMS_ERROR_UNSTART_DISCOVER"}" | true            | false              | 1                     |

@Discover
Scenario: test025_OrderID_098_"Destroy"_Interface dependency testing
	When Discoverui Input Command And Press Send
	| command_one   | command_two |
	| SetWifiApInfo |             |
	Then The response is expected 
    | expected_response                                             | require_approve | require_verify_pwd | verify_whole_response |
    | "{"ResponseCode" : "5", "Message" : "BLE_SMS_ERROR_UNSTART"}" | true            | false              | 1                     |

@Discover
Scenario: test026_OrderID_099_"Destroy"_Interface dependency testing
	When Discoverui Input Command And Press Send
	| command_one | command_two    |
	| StartFind   | {"Status":"0"} |
	Then The response is expected 
    | expected_response                                                     | require_approve | require_verify_pwd | verify_whole_response |
    | "{"ResponseCode" : "10", "Message" : "BLE_SMS_ERROR_UN_SET_WIFI_AP"}" | true            | false              | 1                     |

@Discover
Scenario: test027_OrderID_100_"Destroy"_Interface dependency testing
	When Discoverui Input Command And Press Send
	| command_one | command_two    |
	| StopFind    | {"Status":"0"} |
	Then The response is expected 
    | expected_response                                                       | require_approve | require_verify_pwd | verify_whole_response |
    | "{"ResponseCode" : "8", "Message" : "BLE_SMS_ERROR_UN_START_BE_FOUND"}" | true            | false              | 1                     |

@Discover
Scenario: test028_OrderID_101_"SetWifiApInfo"_Interface dependency testing
	When Discoverui Input Command And Press Send
	| command_one | command_two            |
	| Start       | {"IsHomeGroup":"TRUE"} |
	When Discoverui Input Command And Press Send
	| command_one | command_two    |
	| StartFind   | {"Status":"0"} |
	Then The response is expected 
    | expected_response                                                     | require_approve | require_verify_pwd | verify_whole_response |
    | "{"ResponseCode" : "10", "Message" : "BLE_SMS_ERROR_UN_SET_WIFI_AP"}" | true            | false              | 1                     |

@Discover
Scenario: test029_OrderID_102_"SetWifiApInfo"_Interface dependency testing
	When Discoverui Input Command And Press Send
	| command_one | command_two    |
	| StopFind    | {"Status":"0"} |
	Then The response is expected 
    | expected_response                                                       | require_approve | require_verify_pwd | verify_whole_response |
    | "{"ResponseCode" : "8", "Message" : "BLE_SMS_ERROR_UN_START_BE_FOUND"}" | true            | false              | 1                     |

@Discover
Scenario: test030_OrderID_103_"StartFind"_Interface dependency testing
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
	| command_one   | command_two |
	| SetWifiApInfo | randomPwd   |
	When Discoverui Input Command And Press Send
	| command_one | command_two    |
	| StopFind    | {"Status":"0"} |
	Then The response is expected 
    | expected_response                                                       | require_approve | require_verify_pwd | verify_whole_response |
    | "{"ResponseCode" : "8", "Message" : "BLE_SMS_ERROR_UN_START_BE_FOUND"}" | true            | false              | 1                     |

@Discover
Scenario: test031_OrderID_104_"Start"_Verification of repeatability
	When connectui Input Command And Press Send
	| command_one | command_two    |
	| CloseAP     | {"Status":"0"} |
	When Discoverui Input Command And Press Send
	| command_one | command_two    |
	| Stop        | {"Status":"0"} |
	When Discoverui Input Command And Press Send
	| command_one | command_two            |
	| Start       | {"IsHomeGroup":"TRUE"} |
	When Discoverui Input Command And Press Send
	| command_one | command_two            |
	| Start       | {"IsHomeGroup":"TRUE"} |
	Then The response is expected 
    | expected_response                                               | require_approve | require_verify_pwd | verify_whole_response |
    | "{"ResponseCode" : "2", "Message" : "BLE_SMS_ERROR_DUPLICATE"}" | true            | false              | 1                     |

@Discover
Scenario: test032_OrderID_105_"Stop"_Verification of repeatability
    When Discoverui Input Command And Press Send
	| command_one | command_two    |
	| Stop        | {"Status":"0"} |
	 When Discoverui Input Command And Press Send
	| command_one | command_two    |
	| Stop        | {"Status":"0"} |
	Then The response is expected 
    | expected_response                                               | require_approve | require_verify_pwd | verify_whole_response |
    | "{"ResponseCode" : "2", "Message" : "BLE_SMS_ERROR_DUPLICATE"}" | true            | false              | 1                     |

@Discover
Scenario: test033_OrderID_106_"StartDiscover"_Verification of repeatability
    When Discoverui Input Command And Press Send
	| command_one | command_two            |
	| Start       | {"IsHomeGroup":"TRUE"} |
	When Discoverui Input Command And Press Send
	| command_one   | command_two    |
	| StartDiscover | {"Status":"0"} |
	When Discoverui Input Command And Press Send
	| command_one   | command_two    |
	| StartDiscover | {"Status":"0"} |
	Then The response is expected 
    | expected_response                                               | require_approve | require_verify_pwd | verify_whole_response |
    | "{"ResponseCode" : "2", "Message" : "BLE_SMS_ERROR_DUPLICATE"}" | true            | false              | 1                     |

@Discover
Scenario: test034_OrderID_107_"StopDiscover"_Verification of repeatability
	When Discoverui Input Command And Press Send
	| command_one  | command_two    |
	| StopDiscover | {"Status":"0"} |
	When Discoverui Input Command And Press Send
	| command_one  | command_two    |
	| StopDiscover | {"Status":"0"} |
	Then The response is expected 
    | expected_response                                               | require_approve | require_verify_pwd | verify_whole_response |
    | "{"ResponseCode" : "2", "Message" : "BLE_SMS_ERROR_DUPLICATE"}" | true            | false              | 1                     |

@Discover
Scenario: test035_OrderID_108_"SetWifiApInfo"_Verification of repeatability
    When Discoverui Input Command And Press Send
	| command_one | command_two    |
	| Stop        | {"Status":"0"} |
	When Discoverui Input Command And Press Send
	| command_one | command_two            |
	| Start       | {"IsHomeGroup":"TRUE"} |
	When Discoverui Input Command And Press Send
	| command_one   | command_two                              |
	| SetWifiApInfo | {"SSID":"20230228jia", "PWD":"12345678"} |
	When Discoverui Input Command And Press Send
	| command_one   | command_two                              |
	| SetWifiApInfo | {"SSID":"20230228jia", "PWD":"12345678"} |
	Then The response is expected 
    | expected_response                                               | require_approve | require_verify_pwd | verify_whole_response |
    | "{"ResponseCode" : "2", "Message" : "BLE_SMS_ERROR_DUPLICATE"}" | true            | false              | 1                     |

@Discover
Scenario: test036_OrderID_109_"StartFind"_Verification of repeatability
    When Discoverui Input Command And Press Send
	| command_one | command_two    |
	| StartFind   | {"Status":"0"} |
	When Discoverui Input Command And Press Send
	| command_one | command_two    |
	| StartFind   | {"Status":"0"} |
	Then The response is expected 
    | expected_response                                               | require_approve | require_verify_pwd | verify_whole_response |
    | "{"ResponseCode" : "2", "Message" : "BLE_SMS_ERROR_DUPLICATE"}" | true            | false              | 1                     |

@Discover
Scenario: test037_OrderID_110_"StopFind"_Verification of repeatability
    When Discoverui Input Command And Press Send
	| command_one | command_two    |
	| StopFind    | {"Status":"0"} |
	When Discoverui Input Command And Press Send
	| command_one | command_two    |
	| StopFind    | {"Status":"0"} |
	Then The response is expected 
    | expected_response                                               | require_approve | require_verify_pwd | verify_whole_response |
    | "{"ResponseCode" : "2", "Message" : "BLE_SMS_ERROR_DUPLICATE"}" | true            | false              | 1                     |

@Discover
Scenario: test038_OrderID_111_"Destroy"_Verification of repeatability
    When connectui Input Command And Press Send
	| command_one | command_two    |
	| Stop        | {"Status":"0"} |
    When Discoverui Input Command And Press Send
	| command_one | command_two    |
	| Destroy     | {"Status":"0"} |
	When Discoverui Input Command And Press Send
	| command_one | command_two    |
	| Destroy     | {"Status":"0"} |
	Then The response is expected 
    | expected_response                                               | require_approve | require_verify_pwd | verify_whole_response |
    | "{"ResponseCode" : "2", "Message" : "BLE_SMS_ERROR_DUPLICATE"}" | true            | false              | 1                     |

@Discover
#connect ui stop button
Scenario: test039_close_connect_button
 	When Click Stop Button on The window_name
	| window_name |
	| connect     |

@Discover
#discover ui stop button
Scenario: test040_close_discover_button
	When Click Stop Button on The window_name
	| window_name |
	| discover    |

@Discover
#关闭connect窗口
Scenario: test041_close_connect
	When close connect or discover UI
	| window_name |
	| connect     |

@Discover
#关闭discover窗口
Scenario: test042_close_discover
	When close connect or discover UI
	| window_name |
	| discover    |

@Discover
#关闭主窗口
Scenario: test043_close_unity
	When close connect or discover UI
	| window_name |
	| unity       |