Feature: Interaction_Connect
A short summary of the feature

@Interaction
Scenario Template: Before_open_ldpp_exe_3
	Given Open Ldpp App Window <exe_path>
	And Add Waiting Time <sleep_time1>
	When Click Sign In Button
Scenarios:
	| exe_path                                                        | sleep_time1 |
	| C:\\DCLDPP\\net6.0-windows10.0.19041\\UnitySampleAppWindows.exe | 500         |

@Interaction
Scenario Template: Before_open_window_and_click_start_button1_3
	When Choose <window_name> from The Following Windows Options And Click Test Button 
	When Click Start Button on The <window_name> Window
	Then The response is expected <expected_response> in <require_approve> under <require_verify_pwd> after <verify_whole_response>
Scenarios:
	| window_name | expected_response                         | require_approve | require_verify_pwd | verify_whole_response |
	| connect     | "plugin \| Plugin started successfully!!" | true            | false              | 1                     |

@Interaction
#通过“设置”创建系统热点，再使用ldpp创建AP--创建AP失败
Scenario: Test001_OrderId_001_Create a hotspot in Settings, and then use ldpp to create an AP -- Failed to create an AP
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| Stop        | {"Status":"0"} |
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| Start       | {"Status":"0"} |
	When User set wifi bluetooth mobilehotspot network connect status
		| settings_name | open |
		| mobilehotspot | true |
	When connectui Input Command And Press Send
		| command_one | command_two                                             |
		| CreateAP    | {"SSID":"12345678","PWD":"12345678","MaxConnections":0} |
	Then The response is expected
		| <expected_response>                                                                                                                                                                    | <require_approve> | <require_verify_pwd> | <verify_whole_response> |
		| "{"ResponseCode" : "0", "Message" : "CREATE_AP_OK", "Payload":"{"Status": 3, "Message": "WIFI_START_ERROR_START_ERROR", "SSID": "12345678", "PWD": "12345678", "MaxConnections": 0}"}" | false             | false                | 1                       |
	When User set wifi bluetooth mobilehotspot network connect status
		| settings_name | open  |
		| mobilehotspot | false |
	When connectui Input Command And Press Send
		| command_one | command_two                                              |
		| CreateAP    | {"SSID":"12345678","PWD":"12345678","MaxConnections":99} |
	Then The response is expected
		| <expected_response>                                                                                                                                                      | <require_approve> | <require_verify_pwd> | <verify_whole_response> |
		| "{"ResponseCode" : "0", "Message" : "CREATE_AP_OK", "Payload":"{"Status": 0, "Message": "WIFI_START_OK", "SSID": "12345678", "PWD": "12345678", "MaxConnections": 99}"}" | false             | false                | 1                       |
	
@Interaction
#使用ldpp创建AP,再通过“设置”创建系统热点--系统热点会把ldpp的ap替换掉
Scenario: Test002_OrderId_002_Use ldpp to create aps, and then use Settings to create system hotspots -- The system hotspot will replace the ap of the ldpp
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| Stop        | {"Status":"0"} |
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| Start       | {"Status":"0"} |
	When connectui Input Command And Press Send
		| command_one | command_two                                             |
		| CreateAP    | {"SSID":"12345678","PWD":"12345678","MaxConnections":0} |	
	Then The response is expected
		| <expected_response>                                                                                                                                                     | <require_approve> | <require_verify_pwd> | <verify_whole_response> |
		| "{"ResponseCode" : "0", "Message" : "CREATE_AP_OK", "Payload":"{"Status": 0, "Message": "WIFI_START_OK", "SSID": "12345678", "PWD": "12345678", "MaxConnections": 0}"}" | false             | false                | 1                       |
	When User set wifi bluetooth mobilehotspot network connect status
		| settings_name | open |
		| mobilehotspot | true |
	Then wifidirect_exit and ldpp_ap_ssid same as ap ssid in taskmgr
		| ldpp_ap_name | same_falg | wifidirect_exist |
		| 12345678     | false      | true            |
	When User set wifi bluetooth mobilehotspot network connect status
		| settings_name | open  |
		| mobilehotspot | false |

@Interaction
#关闭蓝牙，使用ldpp创建AP--不影响AP创建
Scenario: Test003_OrderId_003_Bluetooth is turned off and the AP is created using ldpp -- AP creation is not affected 
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| Stop        | {"Status":"0"} |
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| Start       | {"Status":"0"} |
	When User set wifi bluetooth mobilehotspot network connect status
		| settings_name | open  |
		| bluetooth     | false |
	When connectui Input Command And Press Send
		| command_one | command_two                                             |
		| CreateAP    | {"SSID":"12345678","PWD":"12345678","MaxConnections":0} |
	Then The response is expected
		| <expected_response>                                                                                                                                                     | <require_approve> | <require_verify_pwd> | <verify_whole_response> |
		| "{"ResponseCode" : "0", "Message" : "CREATE_AP_OK", "Payload":"{"Status": 0, "Message": "WIFI_START_OK", "SSID": "12345678", "PWD": "12345678", "MaxConnections": 0}"}" | false             | false                | 1                       |
	Then wifidirect_exit and ldpp_ap_ssid same as ap ssid in taskmgr
		| ldpp_ap_name | same_falg | wifidirect_exist |
		| 12345678     | true      | true             |
	When User set wifi bluetooth mobilehotspot network connect status
		| settings_name | open |
		| bluetooth     | true |

@Interaction
#使用ldpp创建AP，关闭蓝牙--AP不受影响
Scenario: Test004_OrderId_004_Create AP using ldpp and turn off Bluetooth -- The AP is not affected
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| Stop        | {"Status":"0"} |
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| Start       | {"Status":"0"} |
	When connectui Input Command And Press Send
		| command_one | command_two                                             |
		| CreateAP    | {"SSID":"12345678","PWD":"12345678","MaxConnections":0} |
	Then The response is expected
		| <expected_response>                                                                                                                                                     | <require_approve> | <require_verify_pwd> | <verify_whole_response> |
		| "{"ResponseCode" : "0", "Message" : "CREATE_AP_OK", "Payload":"{"Status": 0, "Message": "WIFI_START_OK", "SSID": "12345678", "PWD": "12345678", "MaxConnections": 0}"}" | false             | false                | 1                       |
	Then wifidirect_exit and ldpp_ap_ssid same as ap ssid in taskmgr
		| ldpp_ap_name | same_falg | wifidirect_exist |
		| 12345678     | true      | true             |
	When User set wifi bluetooth mobilehotspot network connect status
		| settings_name | open  |
		| bluetooth     | false |
	Then wifidirect_exit and ldpp_ap_ssid same as ap ssid in taskmgr
		| ldpp_ap_name | same_falg | wifidirect_exist |
		| 12345678     | true      | true             |
	When User set wifi bluetooth mobilehotspot network connect status
		| settings_name | open |
		| bluetooth     | true |

@Interaction
#关闭wifi设置，使用ldpp创建AP--无法创建AP
Scenario: Test005_OrderId_005_Turn off wifi Settings and create AP using ldpp -- Unable to create AP
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| Stop        | {"Status":"0"} |
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| Start       | {"Status":"0"} |
	When User set wifi bluetooth mobilehotspot network connect status
		| settings_name | open  |
		| wifi          | false |
	When connectui Input Command And Press Send
		| command_one | command_two                                             |
		| CreateAP    | {"SSID":"12345678","PWD":"12345678","MaxConnections":0} |
	Then The response is expected
		| <expected_response>                                                                                                                                                                    | <require_approve> | <require_verify_pwd> | <verify_whole_response> |
		| "{"ResponseCode" : "0", "Message" : "CREATE_AP_OK", "Payload":"{"Status": 3, "Message": "WIFI_START_ERROR_START_ERROR", "SSID": "12345678", "PWD": "12345678", "MaxConnections": 0}"}" | false             | false                | 1                       |
	When User set wifi bluetooth mobilehotspot network connect status
		| settings_name | open |
		| wifi          | true |
	When User set wifi bluetooth mobilehotspot network connect status
		| settings_name   | open      |
		| network connect | lenovo-5G |
	When connectui Input Command And Press Send
		| command_one | command_two                                             |
		| CreateAP    | {"SSID":"12345678","PWD":"12345678","MaxConnections":0} |
	Then The response is expected
		| <expected_response>                                                                                                                                                     | <require_approve> | <require_verify_pwd> | <verify_whole_response> |
		| "{"ResponseCode" : "0", "Message" : "CREATE_AP_OK", "Payload":"{"Status": 0, "Message": "WIFI_START_OK", "SSID": "12345678", "PWD": "12345678", "MaxConnections": 0}"}" | false             | false                | 1                       |

@Interaction
#断开wifi连接，使用ldpp创建AP--不连wifi也可以成功创建AP
Scenario: Test006_OrderId_006_Disconnect wifi connection and create AP using ldpp -- AP can be successfully created without wifi
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| Stop        | {"Status":"0"} |
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| Start       | {"Status":"0"} |
	When User set wifi bluetooth mobilehotspot network connect status
		| settings_name      | open      |
		| network disconnect | lenovo-5G |
	When connectui Input Command And Press Send
		| command_one | command_two                                             |
		| CreateAP    | {"SSID":"12345678","PWD":"12345678","MaxConnections":0} |
	Then The response is expected
		| <expected_response>                                                                                                                                                     | <require_approve> | <require_verify_pwd> | <verify_whole_response> |
		| "{"ResponseCode" : "0", "Message" : "CREATE_AP_OK", "Payload":"{"Status": 0, "Message": "WIFI_START_OK", "SSID": "12345678", "PWD": "12345678", "MaxConnections": 0}"}" | false             | false                | 1                       |
	Then wifidirect_exit and ldpp_ap_ssid same as ap ssid in taskmgr
		| ldpp_ap_name | same_falg | wifidirect_exist |
		| 12345678     | true      | true             |
	When User set wifi bluetooth mobilehotspot network connect status
		| settings_name   | open      |
		| network connect | lenovo-5G |

@Interaction
#使用ldpp创建AP后关掉系统wifi设置--AP会消失
Scenario: Test007_OrderId_007_Turn off system wifi Settings after creating AP using ldpp -- AP disappearance
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| Stop        | {"Status":"0"} |
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| Start       | {"Status":"0"} |
	When connectui Input Command And Press Send
		| command_one | command_two                                             |
		| CreateAP    | {"SSID":"12345678","PWD":"12345678","MaxConnections":0} |
	Then The response is expected
		| <expected_response>                                                                                                                                                     | <require_approve> | <require_verify_pwd> | <verify_whole_response> |
		| "{"ResponseCode" : "0", "Message" : "CREATE_AP_OK", "Payload":"{"Status": 0, "Message": "WIFI_START_OK", "SSID": "12345678", "PWD": "12345678", "MaxConnections": 0}"}" | false             | false                | 1                       |
	Then wifidirect_exit and ldpp_ap_ssid same as ap ssid in taskmgr
		| ldpp_ap_name | same_falg | wifidirect_exist |
		| 12345678     | true      | true             |
	When User set wifi bluetooth mobilehotspot network connect status
		| settings_name | open  |
		| wifi          | false |
	Then wifidirect_exit and ldpp_ap_ssid same as ap ssid in taskmgr
		| ldpp_ap_name | same_falg | wifidirect_exist |
		|              |           | false            |
	When User set wifi bluetooth mobilehotspot network connect status
		| settings_name | open |
		| wifi          | true |
	When User set wifi bluetooth mobilehotspot network connect status
		| settings_name   | open      |
		| network connect | lenovo-5G |

@Interaction
#使用ldpp创建AP后断开wifi连接--AP不受影响
Scenario: Test008_OrderId_008_Disconnect wifi after creating the AP with ldpp -- the AP is not affected
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| Stop        | {"Status":"0"} |
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| Start       | {"Status":"0"} |
	When connectui Input Command And Press Send
		| command_one | command_two                                             |
		| CreateAP    | {"SSID":"12345678","PWD":"12345678","MaxConnections":0} |
	Then The response is expected
		| <expected_response>                                                                                                                                                     | <require_approve> | <require_verify_pwd> | <verify_whole_response> |
		| "{"ResponseCode" : "0", "Message" : "CREATE_AP_OK", "Payload":"{"Status": 0, "Message": "WIFI_START_OK", "SSID": "12345678", "PWD": "12345678", "MaxConnections": 0}"}" | false             | false                | 1                       |
	Then wifidirect_exit and ldpp_ap_ssid same as ap ssid in taskmgr
		| ldpp_ap_name | same_falg | wifidirect_exist |
		| 12345678     | true      | true             |
	When User set wifi bluetooth mobilehotspot network connect status
		| settings_name      | open      |
		| network disconnect | lenovo-5G |
	Then wifidirect_exit and ldpp_ap_ssid same as ap ssid in taskmgr
		| ldpp_ap_name | same_falg | wifidirect_exist |
		| 12345678     | true      | true             |
	When User set wifi bluetooth mobilehotspot network connect status
		| settings_name   | open      |
		| network connect | lenovo-5G |


@Interaction
#PC连接蓝牙——使用LDPP成功创建AP，PC连接蓝牙——AP不受影响
Scenario: Test009_OrderId_009_The AP is successfully created using LDPP and the PC is connected to Bluetooth -- the AP is not affected
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| Stop        | {"Status":"0"} |
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| Start       | {"Status":"0"} |
	When User set wifi bluetooth mobilehotspot network connect status
		| settings_name | open  |
		| bluetooth     | false |
	When connectui Input Command And Press Send
		| command_one | command_two                                             |
		| CreateAP    | {"SSID":"12345678","PWD":"12345678","MaxConnections":0} |
	Then The response is expected
		| <expected_response>                                                                                                                                                     | <require_approve> | <require_verify_pwd> | <verify_whole_response> |
		| "{"ResponseCode" : "0", "Message" : "CREATE_AP_OK", "Payload":"{"Status": 0, "Message": "WIFI_START_OK", "SSID": "12345678", "PWD": "12345678", "MaxConnections": 0}"}" | false             | false                | 1                       |
	Then wifidirect_exit and ldpp_ap_ssid same as ap ssid in taskmgr
		| ldpp_ap_name | same_falg | wifidirect_exist |
		| 12345678     | true      | true             |
	When User set wifi bluetooth mobilehotspot network connect status
		| settings_name | open |
		| bluetooth     | true |
	Then wifidirect_exit and ldpp_ap_ssid same as ap ssid in taskmgr
		| ldpp_ap_name | same_falg | wifidirect_exist |
		| 12345678     | true      | true             |


@Interaction
#使用ldpp创建ap，休眠（S4）再唤醒PC——AP消失
Scenario: Test010_OrderId_010_Create ap with ldpp, sleep（S4） and wake up PC -- AP disappears
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| Stop        | {"Status":"0"} |
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| Start       | {"Status":"0"} |
	When connectui Input Command And Press Send
		| command_one | command_two                                             |
		| CreateAP    | {"SSID":"12345678","PWD":"12345678","MaxConnections":0} |
	Then The response is expected
		| <expected_response>                                                                                                                                                     | <require_approve> | <require_verify_pwd> | <verify_whole_response> |
		| "{"ResponseCode" : "0", "Message" : "CREATE_AP_OK", "Payload":"{"Status": 0, "Message": "WIFI_START_OK", "SSID": "12345678", "PWD": "12345678", "MaxConnections": 0}"}" | false             | false                | 1                       |
	Then wifidirect_exit and ldpp_ap_ssid same as ap ssid in taskmgr
		| ldpp_ap_name | same_falg | wifidirect_exist |
		| 12345678     | true      | true             |
	When User set system status
		| status |
		| S4     |	
	Then wifidirect_exit and ldpp_ap_ssid same as ap ssid in taskmgr
		| ldpp_ap_name | same_falg | wifidirect_exist |
		|              |           | false            |
	


@Interaction
#使用ldpp创建ap，睡眠（S3）再唤醒PC——AP消失
Scenario: Test011_OrderId_011_Create ap with ldpp, sleep（S3） and wake up PC -- AP disappears
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| Stop        | {"Status":"0"} |
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| Start       | {"Status":"0"} |
	When connectui Input Command And Press Send
		| command_one | command_two                                             |
		| CreateAP    | {"SSID":"12345678","PWD":"12345678","MaxConnections":0} |
	Then The response is expected
		| <expected_response>                                                                                                                                                     | <require_approve> | <require_verify_pwd> | <verify_whole_response> |
		| "{"ResponseCode" : "0", "Message" : "CREATE_AP_OK", "Payload":"{"Status": 0, "Message": "WIFI_START_OK", "SSID": "12345678", "PWD": "12345678", "MaxConnections": 0}"}" | false             | false                | 1                       |	
	Then wifidirect_exit and ldpp_ap_ssid same as ap ssid in taskmgr
		| ldpp_ap_name | same_falg | wifidirect_exist |
		| 12345678     | true      | true             |
	When User set system status
		| status |
		| S3     |
	Then wifidirect_exit and ldpp_ap_ssid same as ap ssid in taskmgr
		| ldpp_ap_name | same_falg | wifidirect_exist |
		|              |           | false            |

@Interaction
#connect ui stop button
Scenario: Turn Off 0_close_connect_button_3
	When Click Stop Button on The window_name
		| window_name |
		| connect     |

@Interaction
#关闭connect窗口
Scenario: Turn Off 1_close_connect_3
	When close connect or discover UI
		| window_name |
		| connect     |

@Interaction
#关闭主窗口
Scenario: Turn Off 2_close_unity_3
	When close connect or discover UI
		| window_name |
		| unity       |