Feature: Connect_PassabilityVerification

A short summary of the feature
Background:
@Connect
Scenario Template: Before_open_ldpp_exe_2
	Given Open Ldpp App Window <exe_path>
	And Add Waiting Time <sleep_time1>
	When Click Sign In Button
Scenarios:
	| exe_path                                                        | sleep_time1 |
	| C:\\DCLDPP\\net6.0-windows10.0.19041\\UnitySampleAppWindows.exe | 500         |
@Connect
Scenario Template: Before_open_window_and_click_start_button1_2
	When Choose <window_name> from The Following Windows Options And Click Test Button 
	When Click Start Button on The <window_name> Window
	Then The response is expected <expected_response> in <require_approve> under <require_verify_pwd> after <verify_whole_response>
Scenarios:
	| window_name | expected_response                         | require_approve | require_verify_pwd | verify_whole_response |
	| connect     | "plugin \| Plugin started successfully!!" | true            | false              | 1                     |

#接口说明
#connectui Input Command And Press Send ：connect ui 输入命令
#Discoverui Input Command And Press Send：Discover ui 输入指令


@Connect
#"Start"启动wifi模块
Scenario: Test001_OrderId_001_"Start" :Wifi Module startup
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| Stop        | {"Status":"0"} |
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| Start       | {"Status":"0"} |
	Then The response is expected
		| <expected_response>                               | <require_approve> | <require_verify_pwd> | <verify_whole_response> |
		| "{"ResponseCode" : "0", "Message" : "COMMON_OK"}" | false             | false                | 1                       |

@Connect
#"Stop"关闭wifi模块
Scenario: Test002_OrderId_002_"Stop" :Wifi module stop
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| Start       | {"Status":"0"} |
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| Stop        | {"Status":"0"} |
	Then The response is expected
		| <expected_response>                             | <require_approve> | <require_verify_pwd> | <verify_whole_response> |
		| "{"ResponseCode" : "0", "Message" : "STOP_OK"}" | false             | false                | 1                       |

@Connect
#"IsApCreated"检查当前是否创建了AP
Scenario: Test003_OrderId_003_"IsApCreated" :Check whether an AP is created
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| CloseAP     | {"Status":"0"} |
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| Start       | {"Status":"0"} |
		#未创建AP
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| IsApCreated | {"Status":"0"} |		
	Then The response is expected
		| <expected_response>                                                                                                                  | <require_approve> | <require_verify_pwd> | <verify_whole_response> |
		| "{"ResponseCode" : "0", "Message" : "IS_AP_CREATED_OK", "Payload":"{"Status": 3, "Message": "WIFI_COMMON_ERROR_WIFI_NOT_RUNNING"}"}" | false             | false                | 1                       |
	When connectui Input Command And Press Send
		| command_one | command_two |
		| CreateAP    | randomPwd   |
	#已创建AP
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| IsApCreated | {"Status":"0"} |
	Then The response is expected
		| <expected_response>                                                                                              | <require_approve> | <require_verify_pwd> | <verify_whole_response> |
		| "{"ResponseCode" : "0", "Message" : "IS_AP_CREATED_OK", "Payload":"{"Status": 0, "Message": "WIFI_COMMON_OK"}"}" | false             | false                | 1                       |
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| CloseAP     | {"Status":"0"} |

@Connect
#"CloseAP"关闭热点
Scenario: Test004_OrderId_005_"CloseAP" :Close AP
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| Start       | {"Status":"0"} |
	When connectui Input Command And Press Send
		| command_one | command_two |
		| CreateAP    | randomPwd   |
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| CloseAP     | {"Status":"0"} |
	Then The response is expected
		| <expected_response>                                                                                       | <require_approve> | <require_verify_pwd> | <verify_whole_response> |
		| "{"ResponseCode" : "0", "Message" : "CLOSE_AP_OK", "Payload":"{"Status": 0, "Message": "WIFI_STOP_OK"}"}" | false             | false                | 1                       |

@Connect
#"CreateAP"创建热点
Scenario: Test005_OrderId_004_"CreateAP" :CreateAP AP
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| CloseAP     | {"Status":"0"} |
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| Start       | {"Status":"0"} |
	#CreateAP（ssid、pwd可用特殊字符)
	When connectui Input Command And Press Send
		| command_one | command_two |
		| CreateAP    | {"SSID":"12A-Za-z0-9@#$%&+-=*_?{}\|[]","PWD":"12A-Za-z0-9@#$%&+-=*_?{}\|[]","MaxConnections":1}   |
	Then The response is expected
		| <expected_response>                                                                                                                                                                                                                     | <require_approve> | <require_verify_pwd> | <verify_whole_response> |
		| "{"ResponseCode" : "0", "Message" : "CREATE_AP_OK", "Payload":"{"Status": 0, "Message": "WIFI_START_OK", "SSID": "12A-Za-z0-9@#$%\\u0026\\u002B-=*_?{}\|[]", "PWD": "12A-Za-z0-9@#$%\\u0026\\u002B-=*_?{}\|[]", "MaxConnections": 1}"}" | false             | false                | 1                       |
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| CloseAP     | {"Status":"0"} |
	#CreateAP(pwd长度取值范围【8，32】)
	When connectui Input Command And Press Send
		| command_one | command_two                                              |
		| CreateAP    | {"SSID":"12345678","PWD":"12345678","MaxConnections":99} |
	Then The response is expected
		| <expected_response>                                                                                                                                                      | <require_approve> | <require_verify_pwd> | <verify_whole_response> |
		| "{"ResponseCode" : "0", "Message" : "CREATE_AP_OK", "Payload":"{"Status": 0, "Message": "WIFI_START_OK", "SSID": "12345678", "PWD": "12345678", "MaxConnections": 99}"}" | false             | false                | 1                       |
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| CloseAP     | {"Status":"0"} |
	When connectui Input Command And Press Send
		| command_one | command_two                                                                                             |
		| CreateAP    | {"SSID":"12345678901234567890123456789012","PWD":"12345678901234567890123456789012","MaxConnections":0} |
	Then The response is expected
		| <expected_response>                                                                                                                                                                                                     | <require_approve> | <require_verify_pwd> | <verify_whole_response> |
		| "{"ResponseCode" : "0", "Message" : "CREATE_AP_OK", "Payload":"{"Status": 0, "Message": "WIFI_START_OK", "SSID": "12345678901234567890123456789012", "PWD": "12345678901234567890123456789012", "MaxConnections": 0}"}" | false             | false                | 1                       |
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| CloseAP     | {"Status":"0"} |
	#CreateAP(ssid长度取1)
	When connectui Input Command And Press Send
		| command_one | command_two                                       |
		| CreateAP    | {"SSID":"1","PWD":"12345678","MaxConnections":99} |
	Then The response is expected
		| <expected_response>                                                                                                                                               | <require_approve> | <require_verify_pwd> | <verify_whole_response> |
		| "{"ResponseCode" : "0", "Message" : "CREATE_AP_OK", "Payload":"{"Status": 0, "Message": "WIFI_START_OK", "SSID": "1", "PWD": "12345678", "MaxConnections": 99}"}" | false             | false                | 1                       |
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| CloseAP     | {"Status":"0"} |

@Connect
#"GetAPInfo"获取AP信息
Scenario: Test006_OrderId_006_"GetAPInfo" :Get AP information
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| CloseAP     | {"Status":"0"} |
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| Start       | {"Status":"0"} |
	#当前未创建AP
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| GetAPInfo   | {"Status":"0"} |
	Then The response is expected
		| <expected_response>                                                                                                                                                            | <require_approve> | <require_verify_pwd> | <verify_whole_response> |
		| "{"ResponseCode" : "0", "Message" : "GET_AP_INFO_OK", "Payload":"{"Status": 3, "Message": "WIFI_COMMON_ERROR_WIFI_NOT_RUNNING", "SSID": "", "PWD": "", "MaxConnections": 0}"}" | false             | false                | 1                       |
	#当前创建了一个AP
	When connectui Input Command And Press Send
		| command_one | command_two                                              |
		| CreateAP    | {"SSID":"12345678","PWD":"12345678","MaxConnections":99} |
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| GetAPInfo   | {"Status":"0"} |
	Then The response is expected
		| <expected_response>                                                                                                                                                         | <require_approve> | <require_verify_pwd> | <verify_whole_response> |
		| "{"ResponseCode" : "0", "Message" : "GET_AP_INFO_OK", "Payload":"{"Status": 0, "Message": "WIFI_COMMON_OK", "SSID": "12345678", "PWD": "12345678", "MaxConnections": 99}"}" | false             | false                | 1                       |
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| CloseAP     | {"Status":"0"} |

@Connect
#"Destroy"kill connect的plugin
Scenario: Test007_OrderId_007_"Destroy" :Kill the Wifi module plugin
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| Start       | {"Status":"0"} |
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| Destroy     | {"Status":"0"} |
	Then The response is expected
		| <expected_response>                                | <require_approve> | <require_verify_pwd> | <verify_whole_response> |
		| "{"ResponseCode" : "0", "Message" : "DESTROY_OK"}" | false             | false                | 1                       |

@Connect
#"IsSupportWifiDirect"查询当前设备是否支持WifiDirect
Scenario: Test008_OrderId_020_"IsSupportWifiDirect" :Get current device whether support WifiDirect
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| Start       | {"Status":"0"} |
	When connectui Input Command And Press Send
		| command_one         | command_two    |
		| IsSupportWifiDirect | {"Status":"0"} |
	Then The response is expected
		| <expected_response>                                                                                                                     | <require_approve> | <require_verify_pwd> | <verify_whole_response> |
		| "{"ResponseCode" : "0", "Message" : "IS_SUPPORT_WD__OK", "Payload":"{"Status": 0, "Message": "WIFI_COMMON_OK", "WifiDirect": "True"}"}" | false             | false                | 1                       |

@Connect
#"GetDevices"查询此时连接AP设备的信息
Scenario: Test009_OrderId_075_"GetDevices" :Get connected wifi-devices' informations
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| CloseAP     | {"Status":"0"} |
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| Start       | {"Status":"0"} |
	#无AP情况下执行GetDevices
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| GetDevices  | {"Status":"0"} |
	Then The response is expected
		| <expected_response>                                                                                                                               | <require_approve> | <require_verify_pwd> | <verify_whole_response> |
		| "{"ResponseCode" : "0", "Message" : "GET_DEVICES_OK", "Payload":"{"Status": 3, "Message": "WIFI_COMMON_ERROR_WIFI_NOT_RUNNING", "Devices": ""}"}" | false             | false                | 1                       |
	#有AP但此时无设备连接AP
	When connectui Input Command And Press Send
		| command_one | command_two |
		| CreateAP    | randomPwd   |
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| GetDevices  | {"Status":"0"} |
	Then The response is expected
		| <expected_response>                                                                                                                                    | <require_approve> | <require_verify_pwd> | <verify_whole_response> |
		| "{"ResponseCode" : "0", "Message" : "GET_DEVICES_OK", "Payload":"{"Status": 0, "Message": "WIFI_COMMON_OK", "Devices": "{"Count" : 0 ,"Clients" : [  ] | false             | false                | 0                       |
	#有AP且此时有设备连接AP——待补充

@Connect
#connect ui stop button
Scenario: Turn Off 0_close_connect_button_2
	When Click Stop Button on The window_name
		| window_name |
		| connect     |

@Connect
#关闭connect窗口
Scenario: Turn Off 1_close_connect_2
	When close connect or discover UI
		| window_name |
		| connect     |

@Connect
#关闭主窗口
Scenario: Turn Off 2_close_unity_2
	When close connect or discover UI
		| window_name |
		| unity       |