Feature: Discover_PassabilityVerification


A short summary of the feature
Background: 
@Discover
#open main ui
Scenario Template: test0_open_ldpp_exe_discover_passability
	Given Open Ldpp App Window <exe_path>
	And Add Waiting Time <sleep_time1>
	When Click Sign In Button
Scenarios:
    | exe_path                                                        | sleep_time1 |
    | C:\\DCLDPP\\net6.0-windows10.0.19041\\UnitySampleAppWindows.exe | 500         |

@Discover
# open main test button,and open connect and discover UI
Scenario Template: test001_open_window_and_click_start_button_discover_passability
	When Choose <window_name> from The Following Windows Options And Click Test Button 
Scenarios:
    | window_name |
    |             |

@Discover
# click connect UI start button
Scenario Template: test002_open_window_and_click_start_button_discover_passability    
	When Click Start Button on The <window_name> Window
	Then The response is expected <expected_response> in <require_approve> under <require_verify_pwd> after <verify_whole_response> 
Scenarios:
    | window_name | expected_response                         | require_approve | require_verify_pwd | verify_whole_response |
    | connect     | "plugin \| Plugin started successfully!!" | true            | false              | 1                     |

@Discover
# click discover UI start button
Scenario Template: test003_open_window_and_click_start_button_discover_passability
	When Click Start Button on The <window_name> Window
	Then The response is expected <expected_response> in <require_approve> under <require_verify_pwd> after <verify_whole_response> 
Scenarios:
    | window_name | expected_response                         | require_approve | require_verify_pwd | verify_whole_response |
    | discover    | "plugin \| Plugin started successfully!!" | true            | false              | 1                     |

#接口说明
#connectui Input Command And Press Send ：connect ui 输入命令
#Discoverui Input Command And Press Send：Discover ui 输入指令

@Discover
Scenario: test004_OrderID_001_"Start"_ble module start interface
	When Discoverui Input Command And Press Send
	| command_one | command_two            |
	| Start       | {"IsHomeGroup":"TRUE"} |
	Then The response is expected 
    | expected_response                                | require_approve | require_verify_pwd | verify_whole_response |
    | "{"ResponseCode" : "0", "Message" : "START_OK"}" | true            | false              | 1                     |

@Discover
Scenario: test005_OrderID_002_"GetDevices"_ble module get connected ble devices' information
	When Discoverui Input Command And Press Send
	| command_one | command_two    |
	| GetDevices  | {"Status":"0"} |
	Then The response is expected 
    | expected_response                                                                                | require_approve | require_verify_pwd | verify_whole_response |
    | "ResponseCode" : "0", "Message" : "COMMON_INTERFACE_OK", "Payload":"{"Status": 0 , "Message": "" | true            | false              | 0                     |

@Discover
Scenario: test006_OrderID_003_"GetBTStatus"_ble module get current device's bluetooth's status
	When Discoverui Input Command And Press Send
	| command_one | command_two    |
	| GetBTStatus | {"Status":"0"} |
	Then The response is expected 
    | expected_response                                                                                                           | require_approve | require_verify_pwd | verify_whole_response |
    | {"ResponseCode" : "0", "Message" : "COMMON_INTERFACE_OK", "Payload":"{"Status": 0 , "Message": "Bluetooth now is opened"}"} | true            | false              | 0                     |

@Discover
Scenario: test007_OrderID_004_"Stop"_ble module stop interface
	When Discoverui Input Command And Press Send
	| command_one | command_two    |
	| Stop        | {"Status":"0"} |
	Then The response is expected 
    | expected_response                                           | require_approve | require_verify_pwd | verify_whole_response |
    | "{"ResponseCode" : "0", "Message" : "COMMON_INTERFACE_OK"}" | true            | false              | 1                     |

@Discover
Scenario: test008_OrderID_005_"StartDiscover"_ble module start scan device interface
	When Discoverui Input Command And Press Send
	| command_one | command_two            |
	| Start       | {"IsHomeGroup":"TRUE"} |
	When connectui Input Command And Press Send
	| command_one | command_two    |
	| Start       | {"Status":"0"} |
	When connectui Input Command And Press Send 
	| command_one | command_two | expected_response |
	| CreateAP    | randomPwd   |                   |
	When Discoverui Input Command And Press Send
	| command_one   | command_two    |
	| StartDiscover | {"Status":"0"} |
	Then The response is expected 
    | expected_response                                                                                    | require_approve | require_verify_pwd | verify_whole_response |
    | "{"ResponseCode" : "0", "Message" : "START_DISCOVER_OK", "Payload":"{"Status": 0 , "Message": ""}"}" | true            | false              | 1                     |

@Discover
Scenario: test009_OrderID_006_"StopDiscover"_ble module stop scan device interface
	When Discoverui Input Command And Press Send
	| command_one  | command_two    |
	| StopDiscover | {"Status":"0"} |
	Then The response is expected 
    | expected_response                                           | require_approve | require_verify_pwd | verify_whole_response |
    | "{"ResponseCode" : "0", "Message" : "COMMON_INTERFACE_OK"}" | true            | false              | 1                     |

#记录当前设备的名字，将{"Status":"0"}，修改成startdiscover
#通过ReadyForLink的command_two区分方法
#readyforlink  command_two = readyforlink       :normal
#readyforlink  command_two = MissingDeviceError :Missing key "Device"
#readyforlink  command_two = MissingNameError   :Missing key "Name"
#readyforlink  command_two = InvalidNameError   :Invalid value of key "Name"
#readyforlink  command_two = MissingBLEError    :Missing key "BLE"
#readyforlink  command_two = InvalidBTError     :Invalid value of key "BT"

@Discover
Scenario: test010_OrderID_007_"ReadyForLink"_ble module connect to peer device interface
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
	| command_one  | command_two  |
	| ReadyForLink | readyforlink |
	Then The response is expected 
    | expected_response                                                                                  | require_approve | require_verify_pwd | verify_whole_response |
    | "{"ResponseCode" : "0", "Message" : "PREPARE_LINK_OK", "Payload":"{"Status": 0 , "Message": ""}"}" | true            | false              | 1                     |
	When is connect status
	| connect status |
	| True           |

@Discover
Scenario:test011_OrderID_010_"IsTrust"_ble module set whether trust party-side's device
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
	| command_one  | command_two  |
	| ReadyForLink | readyforlink |
	When Discoverui Input Command And Press Send
	| command_one | command_two      |
	| IsTrust     | {"IsTrust":"NO"} |
	Then The response is expected 
    | expected_response                                                                                                       | require_approve | require_verify_pwd | verify_whole_response |
    | "{"ResponseCode" : "0", "Message" : "IS_TRUST_INTERFACE_OK", "Payload":"{"Status": 0, "Message": "", "IsTrust":"NO"}"}" | true            | false              | 1                     |

@Discover
Scenario:test012_OrderID_009_"IsTrust"_ble module set whether trust party-side's device
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
	| command_one  | command_two  |
	| ReadyForLink | readyforlink |
	When Discoverui Input Command And Press Send
	| command_one | command_two       |
	| IsTrust     | {"IsTrust":"YES"} |
	Then The response is expected 
    | expected_response                                                                                                        | require_approve | require_verify_pwd | verify_whole_response |
    | "{"ResponseCode" : "0", "Message" : "IS_TRUST_INTERFACE_OK", "Payload":"{"Status": 0, "Message": "", "IsTrust":"YES"}"}" | true            | false              | 1                     |

@Discover
Scenario: test013_OrderID_012_Secondary connection verification
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
	| command_one  | command_two  |
	| ReadyForLink | readyforlink |
	Then The response is expected 
    | expected_response                                                                                  | require_approve | require_verify_pwd | verify_whole_response |
    | "{"ResponseCode" : "0", "Message" : "PREPARE_LINK_OK", "Payload":"{"Status": 0 , "Message": ""}"}" | true            | false              | 1                     |
	When is connect status
	| connect status |
	| True           |
	When Delete LDPP Key

@Discover
Scenario: test014_OrderID_011_"Destroy"_ble module uninitialize interface
	When connectui Input Command And Press Send
	| command_one | command_two    |
	| CloseAP     | {"Status":"0"} |
	When connectui Input Command And Press Send
	| command_one | command_two    |
	| Stop        | {"Status":"0"} |
	When Discoverui Input Command And Press Send
	| command_one  | command_two    |
	| StopDiscover | {"Status":"0"} |
	When Discoverui Input Command And Press Send 
	| command_one | command_two    |
	| Destroy     | {"Status":"0"} |
	Then The response is expected 
    | expected_response                                           | require_approve | require_verify_pwd | verify_whole_response |
    | "{"ResponseCode" : "0", "Message" : "COMMON_INTERFACE_OK"}" | true            | false              | 1                     |

@Discover
Scenario: test015_OrderID_014_"SetPinCode"_ble module set pin code
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
	| command_one   | command_two         |
	| SetWifiApInfo | SetWifiApInfoNormal |
	When get file length 
	| command_one | command_two    |
	When Discoverui Input Command And Press Send
	| command_one | command_two    |
	| StartFind   | {"Status":"0"} |
    When Discoverui Input Command And Press Send
	| command_one | command_two      |
	| SetPinCode  | {"PIN":"222222"} |
	Then The response is expected 
    | expected_response                                                                                                             | require_approve | require_verify_pwd | verify_whole_response |
    | "{"ResponseCode" : "0", "Message" : "SET_PIN_CODE_INTERFACE_OK", "Payload":"{"Status": 0 , "Message": "", "PIN":"222222"}"}" | true            | false              | 1                     |
	When is connect status
	| connect status |
	| False          |

@Discover
Scenario: test016_OrderID_015_"SetPinCode"_ble module set pin code
	When connectui Input Command And Press Send
	| command_one | command_two    |
	| CloseAP     | {"Status":"0"} |
	When Discoverui Input Command And Press Send
	| command_one | command_two    |
	| StopFind    | {"Status":"0"} |
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
	| SetPinCode  | null        |
	Then The response is expected 
    | expected_response                                                                           | require_approve | require_verify_pwd | verify_whole_response |
    | "{"ResponseCode" : "4", "Message" : "SET_PIN_CODE_INTERFACE_ERROR_JSON_DESERIALIZE_ERROR"}" | true            | false              | 1                     |

@Discover
Scenario: test017_OrderID_016_"SetPinCode"_ble module set pin code
	When connectui Input Command And Press Send
	| command_one | command_two    |
	| CloseAP     | {"Status":"0"} |
	When Discoverui Input Command And Press Send
	| command_one | command_two    |
	| StopFind    | {"Status":"0"} |
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
	| SetPinCode  | {}          |
	Then The response is expected 
    | expected_response                                                                  | require_approve | require_verify_pwd | verify_whole_response |
    | "{"ResponseCode" : "5", "Message" : "SET_PIN_CODE_INTERFACE_ERROR_JSON_PIN_NULL"}" | true            | false              | 1                     |

@Discover
Scenario: test018_OrderID_017_"SetPinCode"_ble module set pin code
	When connectui Input Command And Press Send
	| command_one | command_two    |
	| CloseAP     | {"Status":"0"} |
	When Discoverui Input Command And Press Send
	| command_one | command_two    |
	| StopFind    | {"Status":"0"} |
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
	| SetPinCode  |             |
	Then The response is expected 
    | expected_response                                                                   | require_approve | require_verify_pwd | verify_whole_response |
    | "{"ResponseCode" : "2", "Message" : "SET_PIN_CODE_INTERFACE_ERROR_INPUT_IS_EMPTY"}" | true            | false              | 1                     |

@Discover
Scenario: test019_OrderID_018_"SetPinCode"_ble module set pin code
	When connectui Input Command And Press Send
	| command_one | command_two    |
	| CloseAP     | {"Status":"0"} |
	When Discoverui Input Command And Press Send
	| command_one | command_two    |
	| StopFind    | {"Status":"0"} |
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
	| command_one | command_two           |
	| SetPinCode  | {"PIN-Code":"256389"} |
	Then The response is expected 
    | expected_response                                                                  | require_approve | require_verify_pwd | verify_whole_response |
    | "{"ResponseCode" : "5", "Message" : "SET_PIN_CODE_INTERFACE_ERROR_JSON_PIN_NULL"}" | true            | false              | 1                     |

@Discover
Scenario: test020_OrderID_019_"SetPinCode"_ble module set pin code
	When connectui Input Command And Press Send
	| command_one | command_two    |
	| CloseAP     | {"Status":"0"} |
	When Discoverui Input Command And Press Send
	| command_one | command_two    |
	| StopFind    | {"Status":"0"} |
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
	| command_one | command_two    |
	| SetPinCode  | {"PIN":"2589"} |
	Then The response is expected 
    | expected_response                                                                            | require_approve | require_verify_pwd | verify_whole_response |
    | "{"ResponseCode" : "7", "Message" : "SET_PIN_CODE_INTERFACE_ERROR_JSON_PIN_LENGTH_INVALID"}" | true            | false              | 1                     |

@Discover
Scenario: test021_OrderID_020_"SetPinCode"_ble module set pin code
	When connectui Input Command And Press Send
	| command_one | command_two    |
	| CloseAP     | {"Status":"0"} |
	When Discoverui Input Command And Press Send
	| command_one | command_two    |
	| StopFind    | {"Status":"0"} |
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
	| command_one | command_two        |
	| SetPinCode  | {"PIN":"25638974"} |
	Then The response is expected 
    | expected_response                                                                            | require_approve | require_verify_pwd | verify_whole_response |
    | "{"ResponseCode" : "7", "Message" : "SET_PIN_CODE_INTERFACE_ERROR_JSON_PIN_LENGTH_INVALID"}" | true            | false              | 1                     |

@Discover
Scenario: test022_OrderID_021_"SetPinCode"_ble module set pin code
	When connectui Input Command And Press Send
	| command_one | command_two    |
	| CloseAP     | {"Status":"0"} |
	When Discoverui Input Command And Press Send
	| command_one | command_two    |
	| StopFind    | {"Status":"0"} |
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
	| command_one | command_two    |
	| SetPinCode  | {"PIN":256389} |
	Then The response is expected 
    | expected_response                                                                               | require_approve | require_verify_pwd | verify_whole_response |
    | "{"ResponseCode" : "3", "Message" : "SET_PIN_CODE_INTERFACE_ERROR_JSON_DESERIALIZE_EXCEPTION"}" | true            | false              | 1                     |

@Discover
Scenario: test023_OrderID_022_"SetPinCode"_ble module set pin code
	When connectui Input Command And Press Send
	| command_one | command_two    |
	| CloseAP     | {"Status":"0"} |
	When Discoverui Input Command And Press Send
	| command_one | command_two    |
	| StopFind    | {"Status":"0"} |
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
	| command_one | command_two      |
	| SetPinCode  | {"PIN":"aaa389"} |
	Then The response is expected 
    | expected_response                                                                            | require_approve | require_verify_pwd | verify_whole_response |
    | "{"ResponseCode" : "6", "Message" : "SET_PIN_CODE_INTERFACE_ERROR_JSON_INVALID_PIN_VALUE"}" | true            | false              | 1                     |

@Discover
Scenario: test024_OrderID_023_"SetWifiApInfo"_ble modue SetWifiApInfo interface
	When connectui Input Command And Press Send
	| command_one | command_two    |
	| CloseAP     | {"Status":"0"} |
	When Discoverui Input Command And Press Send
	| command_one | command_two    |
	| StopFind    | {"Status":"0"} |
	When connectui Input Command And Press Send 
	| command_one | command_two |
	| CreateAP    | randomPwd   |
	When Discoverui Input Command And Press Send
	| command_one   | command_two         |
	| SetWifiApInfo | SetWifiApInfoNormal |
	Then The response is expected 
    | expected_response                                        | require_approve | require_verify_pwd | verify_whole_response |
    | "{"ResponseCode" : "0", "Message" : "SET_WIFI_INFO_OK"}" | true            | false              | 1                     |

@Discover
Scenario: test025_OrderID_024_"StartFind"_ble module ready to send ble broadcast
	When Discoverui Input Command And Press Send
	| command_one | command_two    |
	| StartFind   | {"Status":"0"} |
	Then The response is expected 
    | expected_response                                                                                      | require_approve | require_verify_pwd | verify_whole_response |
    | "{"ResponseCode" : "0", "Message" : "COMMON_INTERFACE_OK", "Payload":"{"Status": 0 , "Message": ""}"}" | true            | false              | 1                     |

@Discover
Scenario: test026_OrderID_025_"StopFind"_ble module stop send ble broadcast
	When Discoverui Input Command And Press Send
	| command_one | command_two    |
	| StopFind    | {"Status":"0"} |
	Then The response is expected 
    | expected_response                                           | require_approve | require_verify_pwd | verify_whole_response |
    | "{"ResponseCode" : "0", "Message" : "COMMON_INTERFACE_OK"}" | true            | false              | 1                     |

@Discover
Scenario: test027_Stop
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
Scenario: test028_close_connect_button
 	When Click Stop Button on The window_name
	| window_name |
	| connect     |

@Discover
#discover ui stop button
Scenario: test029_close_discover_button
	When Click Stop Button on The window_name
	| window_name |
	| discover    |

@Discover
#关闭connect窗口
Scenario: test030_close_connect
	When close connect or discover UI
	| window_name |
	| connect     |

@Discover
#关闭discover窗口
Scenario: test031_close_discover
	When close connect or discover UI
	| window_name |
	| discover    |

@Discover
#关闭主窗口
Scenario: test032_close_unity
	When close connect or discover UI
	| window_name |
	| unity       |
