Feature: Connect_ParameterAnomalyVerification

A short summary of the feature
Background:
@Connect
Scenario Template: Before_open_ldpp_exe_1
	Given Open Ldpp App Window <exe_path>
	And Add Waiting Time <sleep_time1>
	When Click Sign In Button
Scenarios:
	| exe_path                                                        | sleep_time1 |
	| C:\\DCLDPP\\net6.0-windows10.0.19041\\UnitySampleAppWindows.exe | 500         |
@Connect
Scenario Template: Before_open_window_and_click_start_button1_1
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
#"Start"启动wifi模块参数异常验证_无效的Json对象null
Scenario: Test001_OrderId_008_"Start" :Invalid Json Object null
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| Stop        | {"Status":"0"} |
	When connectui Input Command And Press Send
		| command_one | command_two |
		| Start       | null        |
	Then The response is expected
		| <expected_response>                                                         | <require_approve> | <require_verify_pwd> | <verify_whole_response> |
		| "{"ResponseCode" : "4", "Message" : "COMMON_ERROR_JSON_DESERIALIZE_ERROR"}" | false             | false                | 1                       |

@Connect
#"Start"启动wifi模块参数异常验证_空Json对象{}
Scenario: Test002_OrderId_009_"Start" :Empty Json Object {}
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| Stop        | {"Status":"0"} |
	When connectui Input Command And Press Send
		| command_one | command_two |
		| Start       | {}          |
	Then The response is expected
		| <expected_response>                                                   | <require_approve> | <require_verify_pwd> | <verify_whole_response> |
		| "{"ResponseCode" : "5", "Message" : "COMMON_ERROR_JSON_STATUS_NULL"}" | false             | false                | 1                       |
		
@Connect
#"Start"启动wifi模块参数异常验证_什么也不填
Scenario: Test003_OrderId_010_"Start" :Input Nothing
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| Stop        | {"Status":"0"} |
	When connectui Input Command And Press Send
		| command_one | command_two |
		| Start       |             |
	Then The response is expected
		| <expected_response>                                                  | <require_approve> | <require_verify_pwd> | <verify_whole_response> |
		| "{"ResponseCode" : "2", "Message" : "COMMON_ERROR_INPUT_IS_EMPTY"}" | false             | false                | 1                       |

@Connect
#"Start"启动wifi模块参数异常验证_Status值错误
Scenario: Test004_OrderId_011_"Start" :The value of key "Status" is wrong
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| Stop        | {"Status":"0"} |
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| Start       | {"Status":"1"} |
	Then The response is expected
		| <expected_response>                                                            | <require_approve> | <require_verify_pwd> | <verify_whole_response> |
		| "{"ResponseCode" : "6", "Message" : "COMMON_ERROR_JSON_INVALID_STATUS_VALUE"}" | false             | false                | 1                       |

@Connect
#"Start"启动wifi模块参数异常验证_Status拼写错误
Scenario: Test005_OrderId_012_"Start" :Missing key "Status"
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| Stop        | {"Status":"0"} |
	When connectui Input Command And Press Send
		| command_one | command_two  |
		| Start       | {"Stus":"1"} |
	Then The response is expected
		| <expected_response>                                                   | <require_approve> | <require_verify_pwd> | <verify_whole_response> |
		| "{"ResponseCode" : "5", "Message" : "COMMON_ERROR_JSON_STATUS_NULL"}" | false             | false                | 1                       |

@Connect
#"Start"启动wifi模块参数异常验证_“Status”值的类型错误
Scenario: Test006_OrderId_013_"Start" :The value's type of key "Status" is wrong
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| Stop        | {"Status":"0"} |
	When connectui Input Command And Press Send
		| command_one | command_two  |
		| Start       | {"Status":1} |
	Then The response is expected
		| <expected_response>                                                             | <require_approve> | <require_verify_pwd> | <verify_whole_response> |
		| "{"ResponseCode" : "3", "Message" : "COMMON_ERROR_JSON_DESERIALIZE_EXCEPTION"}" | false             | false                | 1                       |

@Connect
#"Stop"关闭wifi模块参数异常验证_无效的Json对象null
Scenario: Test007_OrderId_014_"Stop" :Invalid Json Object null
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| Start       | {"Status":"0"} |
	When connectui Input Command And Press Send
		| command_one | command_two |
		| Stop        | null        |
	Then The response is expected
		| <expected_response>                                                       | <require_approve> | <require_verify_pwd> | <verify_whole_response> |
		| "{"ResponseCode" : "4", "Message" : "STOP_ERROR_JSON_DESERIALIZE_ERROR"}" | false             | false                | 1                       |

@Connect
#"Stop"关闭wifi模块参数异常验证_空Json对象{}
Scenario: Test008_OrderId_019_"Stop" :Empty Json Object {}
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| Start       | {"Status":"0"} |
	When connectui Input Command And Press Send
		| command_one | command_two |
		| Stop        | {}          |
	Then The response is expected
		| <expected_response>                                                 | <require_approve> | <require_verify_pwd> | <verify_whole_response> |
		| "{"ResponseCode" : "5", "Message" : "STOP_ERROR_JSON_STATUS_NULL"}" | false             | false                | 1                       |
		
@Connect
#"Stop"关闭wifi模块参数异常验证_什么也不填
Scenario: Test009_OrderId_015_"Stop" :Input Nothing
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| Start       | {"Status":"0"} |
	When connectui Input Command And Press Send
		| command_one | command_two |
		| Stop        |             |
	Then The response is expected
		| <expected_response>                                               | <require_approve> | <require_verify_pwd> | <verify_whole_response> |
		| "{"ResponseCode" : "2", "Message" : "STOP_ERROR_INPUT_IS_EMPTY"}" | false             | false                | 1                       |

@Connect
#"Stop"关闭wifi模块参数异常验证_Status值错误
Scenario: Test010_OrderId_016_"Stop" :The value of key "Status" is wrong
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| Start       | {"Status":"0"} |
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| Stop        | {"Status":"1"} |
	Then The response is expected
		| <expected_response>                                                          | <require_approve> | <require_verify_pwd> | <verify_whole_response> |
		| "{"ResponseCode" : "6", "Message" : "STOP_ERROR_JSON_INVALID_STATUS_VALUE"}" | false             | false                | 1                       |

@Connect
#"Stop"关闭wifi模块参数异常验证_Status拼写错误
Scenario: Test011_OrderId_017_"Stop" :Missing key "Status"
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| Start       | {"Status":"0"} |
	When connectui Input Command And Press Send
		| command_one | command_two  |
		| Stop        | {"Stus":"1"} |
	Then The response is expected
		| <expected_response>                                                 | <require_approve> | <require_verify_pwd> | <verify_whole_response> |
		| "{"ResponseCode" : "5", "Message" : "STOP_ERROR_JSON_STATUS_NULL"}" | false             | false                | 1                       |

@Connect
#"Stop"关闭wifi模块参数异常验证_“Status”值的类型错误
Scenario: Test012_OrderId_018_"Stop" :The value's type of key "Status" is wrong
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| Start       | {"Status":"0"} |
	When connectui Input Command And Press Send
		| command_one | command_two  |
		| Stop        | {"Status":1} |
	Then The response is expected
		| <expected_response>                                                           | <require_approve> | <require_verify_pwd> | <verify_whole_response> |
		| "{"ResponseCode" : "3", "Message" : "STOP_ERROR_JSON_DESERIALIZE_EXCEPTION"}" | false             | false                | 1                       |

@Connect
#"IsApCreated"参数异常验证_无效的Json对象null
Scenario: Test013_OrderId_021_"IsApCreated" :Invalid Json Object null
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| CloseAP     | {"Status":"0"} |
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| Start       | {"Status":"0"} |
	When connectui Input Command And Press Send
		| command_one | command_two |
		| CreateAP    | randomPwd   |
	When connectui Input Command And Press Send
		| command_one | command_two |
		| IsApCreated | null        |
	Then The response is expected
		| <expected_response>                                                                | <require_approve> | <require_verify_pwd> | <verify_whole_response> |
		| "{"ResponseCode" : "4", "Message" : "IS_AP_CREATED_ERROR_JSON_DESERIALIZE_ERROR"}" | false             | false                | 1                       |
	
@Connect
#"IsApCreated"参数异常验证_什么也不填
Scenario: Test014_OrderId_022_"IsApCreated" :Input Nothing
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| CloseAP     | {"Status":"0"} |
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| Start       | {"Status":"0"} |
	When connectui Input Command And Press Send
		| command_one | command_two |
		| CreateAP    | randomPwd   |
	When connectui Input Command And Press Send
		| command_one | command_two |
		| IsApCreated |             |
	Then The response is expected
		| <expected_response>                                                        | <require_approve> | <require_verify_pwd> | <verify_whole_response> |
		| "{"ResponseCode" : "2", "Message" : "IS_AP_CREATED_ERROR_INPUT_IS_EMPTY"}" | false             | false                | 1                       |
	
@Connect
#"IsApCreated"参数异常验证_Status值错误
Scenario: Test015_OrderId_023_"IsApCreated" :The value of key "Status" is wrong
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| CloseAP     | {"Status":"0"} |
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| Start       | {"Status":"0"} |
	When connectui Input Command And Press Send
		| command_one | command_two |
		| CreateAP    | randomPwd   |
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| IsApCreated | {"Status":"1"} |
	Then The response is expected
		| <expected_response>                                                                   | <require_approve> | <require_verify_pwd> | <verify_whole_response> |
		| "{"ResponseCode" : "6", "Message" : "IS_AP_CREATED_ERROR_JSON_INVALID_STATUS_VALUE"}" | false             | false                | 1                       |
	
@Connect
#"IsApCreated"参数异常验证_Status拼写错误
Scenario: Test016_OrderId_024_"IsApCreated" :Missing key "Status"
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| CloseAP     | {"Status":"0"} |
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| Start       | {"Status":"0"} |
	When connectui Input Command And Press Send
		| command_one | command_two |
		| CreateAP    | randomPwd   |
	When connectui Input Command And Press Send
		| command_one | command_two  |
		| IsApCreated | {"Stus":"0"} |
	Then The response is expected
		| <expected_response>                                                          | <require_approve> | <require_verify_pwd> | <verify_whole_response> |
		| "{"ResponseCode" : "5", "Message" : "IS_AP_CREATED_ERROR_JSON_STATUS_NULL"}" | false             | false                | 1                       |
	
@Connect
#"IsApCreated"参数异常验证_“Status”值的类型错误
Scenario: Test017_OrderId_025_"IsApCreated" :The value's type of key "Status" is wrong
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| CloseAP     | {"Status":"0"} |
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| Start       | {"Status":"0"} |
	When connectui Input Command And Press Send
		| command_one | command_two |
		| CreateAP    | randomPwd   |
	When connectui Input Command And Press Send
		| command_one | command_two  |
		| IsApCreated | {"Status":0} |
	Then The response is expected
		| <expected_response>                                                                    | <require_approve> | <require_verify_pwd> | <verify_whole_response> |
		| "{"ResponseCode" : "3", "Message" : "IS_AP_CREATED_ERROR_JSON_DESERIALIZE_EXCEPTION"}" | false             | false                | 1                       |
	
@Connect
#"IsApCreated"参数异常验证_空Json对象{}
Scenario: Test018_OrderId_026_"IsApCreated" :Invalid Json Object:{}
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| CloseAP     | {"Status":"0"} |
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| Start       | {"Status":"0"} |
	When connectui Input Command And Press Send
		| command_one | command_two |
		| CreateAP    | randomPwd   |
	When connectui Input Command And Press Send
		| command_one | command_two |
		| IsApCreated | {}          |
	Then The response is expected
		| <expected_response>                                                          | <require_approve> | <require_verify_pwd> | <verify_whole_response> |
		| "{"ResponseCode" : "5", "Message" : "IS_AP_CREATED_ERROR_JSON_STATUS_NULL"}" | false             | false                | 1                       |

@Connect
#"CreateAP"参数异常验证_无效的Json对象null
Scenario: Test019_OrderId_027_"CreateAP" :Invalid Json Object:null
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| CloseAP     | {"Status":"0"} |
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| Start       | {"Status":"0"} |
	When connectui Input Command And Press Send
		| command_one | command_two |
		| CreateAP    | null        |
	Then The response is expected
		| <expected_response>                                                            | <require_approve> | <require_verify_pwd> | <verify_whole_response> |
		| "{"ResponseCode" : "4", "Message" : "CREATE_AP_ERROR_JSON_DESERIALIZE_ERROR"}" | false             | false                | 1                       |

@Connect
#"CreateAP"参数异常验证_空Json对象{}
Scenario: Test020_OrderId_028_"CreateAP" :Invalid Json Object:{}
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| CloseAP     | {"Status":"0"} |
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| Start       | {"Status":"0"} |
	When connectui Input Command And Press Send
		| command_one | command_two |
		| CreateAP    | {}          |
	Then The response is expected
		| <expected_response>                                                    | <require_approve> | <require_verify_pwd> | <verify_whole_response> |
		| "{"ResponseCode" : "5", "Message" : "CREATE_AP_ERROR_JSON_SSID_NULL"}" | false             | false                | 1                       |

@Connect
#"CreateAP"参数异常验证_"SSID"值无效
Scenario: Test021_OrderId_029_"CreateAP" :Invalid value of key "SSID"
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| CloseAP     | {"Status":"0"} |
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| Start       | {"Status":"0"} |
	When connectui Input Command And Press Send
		| command_one | command_two                    |
		| CreateAP    | {"SSID":qweerer, "PWD":"1234"} |
	Then The response is expected
		| <expected_response>                                                                | <require_approve> | <require_verify_pwd> | <verify_whole_response> |
		| "{"ResponseCode" : "3", "Message" : "CREATE_AP_ERROR_JSON_DESERIALIZE_EXCEPTION"}" | false             | false                | 1                       |

@Connect
#"CreateAP"参数异常验证_缺少参数"SSID"
Scenario: Test022_OrderId_030_"CreateAP" :Missing key "SSID"
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| CloseAP     | {"Status":"0"} |
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| Start       | {"Status":"0"} |
	When connectui Input Command And Press Send
		| command_one | command_two                      |
		| CreateAP    | {"SS":"asdfg", "PWD":"12345678"} |
	Then The response is expected
		| <expected_response>                                                    | <require_approve> | <require_verify_pwd> | <verify_whole_response> |
		| "{"ResponseCode" : "5", "Message" : "CREATE_AP_ERROR_JSON_SSID_NULL"}" | false             | false                | 1                       |

@Connect
#"CreateAP"参数异常验证_缺少参数"PWD"
Scenario: Test023_OrderId_031_"CreateAP" :Missing key "PWD"
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| CloseAP     | {"Status":"0"} |
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| Start       | {"Status":"0"} |
	When connectui Input Command And Press Send
		| command_one | command_two                             |
		| CreateAP    | {"SSID":"asdfg", "Password":"12345678"} |
	Then The response is expected
		| <expected_response>                                                   | <require_approve> | <require_verify_pwd> | <verify_whole_response> |
		| "{"ResponseCode" : "6", "Message" : "CREATE_AP_ERROR_JSON_PWD_NULL"}" | false             | false                | 1                       |

@Connect
#"CreateAP"参数异常验证_什么都不填
Scenario: Test024_OrderId_032_"CreateAP" :Input Nothing
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| CloseAP     | {"Status":"0"} |
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| Start       | {"Status":"0"} |
	When connectui Input Command And Press Send
		| command_one | command_two |
		| CreateAP    |             |
	Then The response is expected
		| <expected_response>                                                    | <require_approve> | <require_verify_pwd> | <verify_whole_response> |
		| "{"ResponseCode" : "2", "Message" : "CREATE_AP_ERROR_INPUT_IS_EMPTY"}" | false             | false                | 1                       |

@Connect
#"CreateAP"参数异常验证_"PWD"值长度小于8
Scenario: Test025_OrderId_033_"CreateAP" :The length of value of key "PWD" is shorter than 8
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| CloseAP     | {"Status":"0"} |
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| Start       | {"Status":"0"} |
	When connectui Input Command And Press Send
		| command_one | command_two                       |
		| CreateAP    | {"SSID":"asdfg", "PWD":"1234567"} |
	Then The response is expected
		| <expected_response>                                                             | <require_approve> | <require_verify_pwd> | <verify_whole_response> |
		| "{"ResponseCode" : "7", "Message" : "CREATE_AP_ERROR_JSON_PWD_LENGTH_INVALID"}" | false             | false                | 1                       |

@Connect
#"CreateAP"参数异常验证_"PWD"值长度大于32
Scenario: Test026_OrderId_034_"CreateAP" :“The length of value of key "PWD" is longer than 32
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| CloseAP     | {"Status":"0"} |
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| Start       | {"Status":"0"} |
	When connectui Input Command And Press Send
		| command_one | command_two                                                 |
		| CreateAP    | {"SSID":"asdfg", "PWD":"123456789012345678901234567890123"} |
	Then The response is expected
		| <expected_response>                                                             | <require_approve> | <require_verify_pwd> | <verify_whole_response> |
		| "{"ResponseCode" : "7", "Message" : "CREATE_AP_ERROR_JSON_PWD_LENGTH_INVALID"}" | false             | false                | 1                       |

@Connect
#"CreateAP"参数异常验证_"PWD"值包含汉字
Scenario: Test027_OrderId_035_"CreateAP" :"PWD" contains Chinese characters
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| CloseAP     | {"Status":"0"} |
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| Start       | {"Status":"0"} |
	When connectui Input Command And Press Send
		| command_one | command_two                        |
		| CreateAP    | {"SSID":"asdfg", "PWD":"哈哈哈哈哈哈哈哈"} |
	Then The response is expected
		| <expected_response>                                                      | <require_approve> | <require_verify_pwd> | <verify_whole_response> |
		| "{"ResponseCode" : "9", "Message" : "CREATE_AP_ERROR_JSON_PWD_INVALID"}" | false             | false                | 1                       |

@Connect
#"CreateAP"参数异常验证_"SSID"值包含汉字
Scenario: Test028_OrderId_036_"CreateAP" :"SSID" contains Chinese characters
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| CloseAP     | {"Status":"0"} |
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| Start       | {"Status":"0"} |
	When connectui Input Command And Press Send
		| command_one | command_two                             |
		| CreateAP    | {"SSID":"asdf哈哈","PWD":"123@#$$asdasd"} |
	Then The response is expected
		| <expected_response>                                                       | <require_approve> | <require_verify_pwd> | <verify_whole_response> |
		| "{"ResponseCode" : "8", "Message" : "CREATE_AP_ERROR_JSON_SSID_INVALID"}" | false             | false                | 1                       |

@Connect
#"CloseAP"参数异常验证_无效的Json对象null
Scenario: Test029_OrderId_037_"CloseAP" :Invalid Json Object:null
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| Start       | {"Status":"0"} |
	When connectui Input Command And Press Send
		| command_one | command_two |
		| CreateAP    | randomPwd   |
	When connectui Input Command And Press Send
		| command_one | command_two |
		| CloseAP     | null        |
	Then The response is expected
		| <expected_response>                                                           | <require_approve> | <require_verify_pwd> | <verify_whole_response> |
		| "{"ResponseCode" : "4", "Message" : "CLOSE_AP_ERROR_JSON_DESERIALIZE_ERROR"}" | false             | false                | 1                       |

@Connect
#"CloseAP"参数异常验证_什么也不填
Scenario: Test030_OrderId_038_"CloseAP" :Input Nothing
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| Start       | {"Status":"0"} |
	When connectui Input Command And Press Send
		| command_one | command_two |
		| CreateAP    | randomPwd   |
	When connectui Input Command And Press Send
		| command_one | command_two |
		| CloseAP     |             |
	Then The response is expected
		| <expected_response>                                                   | <require_approve> | <require_verify_pwd> | <verify_whole_response> |
		| "{"ResponseCode" : "2", "Message" : "CLOSE_AP_ERROR_INPUT_IS_EMPTY"}" | false             | false                | 1                       |

@Connect
#"CloseAP"参数异常验证_Status值错误
Scenario: Test031_OrderId_039_"CloseAP" :The value of key "Status" is wrong
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| Start       | {"Status":"0"} |
	When connectui Input Command And Press Send
		| command_one | command_two |
		| CreateAP    | randomPwd   |
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| CloseAP     | {"Status":"1"} |
	Then The response is expected
		| <expected_response>                                                              | <require_approve> | <require_verify_pwd> | <verify_whole_response> |
		| "{"ResponseCode" : "6", "Message" : "CLOSE_AP_ERROR_JSON_INVALID_STATUS_VALUE"}" | false             | false                | 1                       |

@Connect
#"CloseAP"参数异常验证_Status拼写错误
Scenario: Test032_OrderId_040_"CloseAP" :Missing key "Status"
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| Start       | {"Status":"0"} |
	When connectui Input Command And Press Send
		| command_one | command_two |
		| CreateAP    | randomPwd   |
	When connectui Input Command And Press Send
		| command_one | command_two  |
		| CloseAP     | {"Stus":"0"} |
	Then The response is expected
		| <expected_response>                                                     | <require_approve> | <require_verify_pwd> | <verify_whole_response> |
		| "{"ResponseCode" : "5", "Message" : "CLOSE_AP_ERROR_JSON_STATUS_NULL"}" | false             | false                | 1                       |

@Connect
#"CloseAP"参数异常验证_“Status”值的类型错误
Scenario: Test033_OrderId_041_"CloseAP" :The value's type of key "Status" is wrong
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| Start       | {"Status":"0"} |
	When connectui Input Command And Press Send
		| command_one | command_two |
		| CreateAP    | randomPwd   |
	When connectui Input Command And Press Send
		| command_one | command_two  |
		| CloseAP     | {"Status":0} |
	Then The response is expected
		| <expected_response>                                                               | <require_approve> | <require_verify_pwd> | <verify_whole_response> |
		| "{"ResponseCode" : "3", "Message" : "CLOSE_AP_ERROR_JSON_DESERIALIZE_EXCEPTION"}" | false             | false                | 1                       |

@Connect
#"CloseAP"参数异常验证_空Json对象{}
Scenario: Test034_OrderId_042_"CloseAP" :Invalid Json Object:{}
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| Start       | {"Status":"0"} |
	When connectui Input Command And Press Send
		| command_one | command_two |
		| CreateAP    | randomPwd   |
	When connectui Input Command And Press Send
		| command_one | command_two |
		| CloseAP     | {}          |
	Then The response is expected
		| <expected_response>                                                     | <require_approve> | <require_verify_pwd> | <verify_whole_response> |
		| "{"ResponseCode" : "5", "Message" : "CLOSE_AP_ERROR_JSON_STATUS_NULL"}" | false             | false                | 1                       |

@Connect
#"GetAPInfo"参数异常验证_无效的Json对象null
Scenario: Test035_OrderId_043_"GetAPInfo" :Invalid Json Object:null
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| CloseAP     | {"Status":"0"} |
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| Start       | {"Status":"0"} |
	When connectui Input Command And Press Send
		| command_one | command_two                                              |
		| CreateAP    | {"SSID":"12345678","PWD":"12345678","MaxConnections":99} |
	When connectui Input Command And Press Send
		| command_one | command_two |
		| GetAPInfo   | null        |
	Then The response is expected
		| <expected_response>                                                              | <require_approve> | <require_verify_pwd> | <verify_whole_response> |
		| "{"ResponseCode" : "4", "Message" : "GET_AP_INFO_ERROR_JSON_DESERIALIZE_ERROR"}" | false             | false                | 1                       |

@Connect
#"GetAPInfo"参数异常验证_什么都不填
Scenario: Test036_OrderId_044_"GetAPInfo" :Input Nothing
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| CloseAP     | {"Status":"0"} |
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| Start       | {"Status":"0"} |
	When connectui Input Command And Press Send
		| command_one | command_two                                              |
		| CreateAP    | {"SSID":"12345678","PWD":"12345678","MaxConnections":99} |
	When connectui Input Command And Press Send
		| command_one | command_two |
		| GetAPInfo   |             |
	Then The response is expected
		| <expected_response>                                                      | <require_approve> | <require_verify_pwd> | <verify_whole_response> |
		| "{"ResponseCode" : "2", "Message" : "GET_AP_INFO_ERROR_INPUT_IS_EMPTY"}" | false             | false                | 1                       |

@Connect
#"GetAPInfo"参数异常验证_Status值错误
Scenario: Test037_OrderId_045_"GetAPInfo" :The value of key "Status" is wrong
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| CloseAP     | {"Status":"0"} |
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| Start       | {"Status":"0"} |
	When connectui Input Command And Press Send
		| command_one | command_two                                              |
		| CreateAP    | {"SSID":"12345678","PWD":"12345678","MaxConnections":99} |
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| GetAPInfo   | {"Status":"1"} |
	Then The response is expected
		| <expected_response>                                                                 | <require_approve> | <require_verify_pwd> | <verify_whole_response> |
		| "{"ResponseCode" : "6", "Message" : "GET_AP_INFO_ERROR_JSON_INVALID_STATUS_VALUE"}" | false             | false                | 1                       |

@Connect
#"GetAPInfo"参数异常验证_Status拼写错误
Scenario: Test038_OrderId_046_"GetAPInfo" :Missing key "Status"
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| CloseAP     | {"Status":"0"} |
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| Start       | {"Status":"0"} |
	When connectui Input Command And Press Send
		| command_one | command_two                                              |
		| CreateAP    | {"SSID":"12345678","PWD":"12345678","MaxConnections":99} |
	When connectui Input Command And Press Send
		| command_one | command_two  |
		| GetAPInfo   | {"Stus":"0"} |
	Then The response is expected
		| <expected_response>                                                        | <require_approve> | <require_verify_pwd> | <verify_whole_response> |
		| "{"ResponseCode" : "5", "Message" : "GET_AP_INFO_ERROR_JSON_STATUS_NULL"}" | false             | false                | 1                       |

@Connect
#"GetAPInfo"参数异常验证_“Status”值的类型错误
Scenario: Test039_OrderId_047_"GetAPInfo" :The value's type of key "Status" is wrong
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| CloseAP     | {"Status":"0"} |
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| Start       | {"Status":"0"} |
	When connectui Input Command And Press Send
		| command_one | command_two                                              |
		| CreateAP    | {"SSID":"12345678","PWD":"12345678","MaxConnections":99} |
	When connectui Input Command And Press Send
		| command_one | command_two  |
		| GetAPInfo   | {"Status":0} |
	Then The response is expected
		| <expected_response>                                                                  | <require_approve> | <require_verify_pwd> | <verify_whole_response> |
		| "{"ResponseCode" : "3", "Message" : "GET_AP_INFO_ERROR_JSON_DESERIALIZE_EXCEPTION"}" | false             | false                | 1                       |

@Connect
#"GetAPInfo"参数异常验证_空Json对象{}
Scenario: Test040_OrderId_076_"GetAPInfo" :Empty Json Object {}
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| CloseAP     | {"Status":"0"} |
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| Start       | {"Status":"0"} |
	When connectui Input Command And Press Send
		| command_one | command_two                                              |
		| CreateAP    | {"SSID":"12345678","PWD":"12345678","MaxConnections":99} |
	When connectui Input Command And Press Send
		| command_one | command_two |
		| GetAPInfo   | {}          |
	Then The response is expected
		| <expected_response>                                                        | <require_approve> | <require_verify_pwd> | <verify_whole_response> |
		| "{"ResponseCode" : "5", "Message" : "GET_AP_INFO_ERROR_JSON_STATUS_NULL"}" | false             | false                | 1                       |

@Connect
#"Destroy"参数异常验证_无效的Json对象null
Scenario: Test041_OrderId_048_"Destroy" :Invalid Json Object:null
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| Start       | {"Status":"0"} |
	When connectui Input Command And Press Send
		| command_one | command_two |
		| Destroy     | null        |
	Then The response is expected
		| <expected_response>                                                          | <require_approve> | <require_verify_pwd> | <verify_whole_response> |
		| "{"ResponseCode" : "4", "Message" : "DESTROY_ERROR_JSON_DESERIALIZE_ERROR"}" | false             | false                | 1                       |

@Connect
#"Destroy"参数异常验证_什么都不填
Scenario: Test042_OrderId_049_"Destroy":Input Nothing
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| Start       | {"Status":"0"} |
	When connectui Input Command And Press Send
		| command_one | command_two |
		| Destroy     |             |
	Then The response is expected
		| <expected_response>                                                  | <require_approve> | <require_verify_pwd> | <verify_whole_response> |
		| "{"ResponseCode" : "2", "Message" : "DESTROY_ERROR_INPUT_IS_EMPTY"}" | false             | false                | 1                       |

@Connect
#"Destroy"参数异常验证_Status值错误
Scenario: Test043_OrderId_050_"Destroy":The value of key "Status" is wrong
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| Start       | {"Status":"0"} |
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| Destroy     | {"Status":"1"} |
	Then The response is expected
		| <expected_response>                                                             | <require_approve> | <require_verify_pwd> | <verify_whole_response> |
		| "{"ResponseCode" : "6", "Message" : "DESTROY_ERROR_JSON_INVALID_STATUS_VALUE"}" | false             | false                | 1                       |

@Connect
#"Destroy"参数异常验证_Status拼写错误
Scenario: Test044_OrderId_051_"Destroy":Missing key "Status"
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| Start       | {"Status":"0"} |
	When connectui Input Command And Press Send
		| command_one | command_two  |
		| Destroy     | {"Stus":"0"} |
	Then The response is expected
		| <expected_response>                                                    | <require_approve> | <require_verify_pwd> | <verify_whole_response> |
		| "{"ResponseCode" : "5", "Message" : "DESTROY_ERROR_JSON_STATUS_NULL"}" | false             | false                | 1                       |

@Connect
#"Destroy"参数异常验证_“Status”值的类型错误
Scenario: Test045_OrderId_052_"Destroy":The value's type of key "Status" is wrong
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| Start       | {"Status":"0"} |
	When connectui Input Command And Press Send
		| command_one | command_two  |
		| Destroy     | {"Status":0} |
	Then The response is expected
		| <expected_response>                                                              | <require_approve> | <require_verify_pwd> | <verify_whole_response> |
		| "{"ResponseCode" : "3", "Message" : "DESTROY_ERROR_JSON_DESERIALIZE_EXCEPTION"}" | false             | false                | 1                       |

@Connect
#"Destroy"参数异常验证_空Json对象{}
Scenario: Test046_OrderId_053_"Destroy":Invalid Json Object:{}
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| Start       | {"Status":"0"} |
	When connectui Input Command And Press Send
		| command_one | command_two |
		| Destroy     | {}          |
	Then The response is expected
		| <expected_response>                                                    | <require_approve> | <require_verify_pwd> | <verify_whole_response> |
		| "{"ResponseCode" : "5", "Message" : "DESTROY_ERROR_JSON_STATUS_NULL"}" | false             | false                | 1                       |

@Connect
#"GetDevices"参数异常验证_无效的Json对象null
Scenario: Test047_OrderId_077_"GetDevices":Invalid Json Object:null
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| CloseAP     | {"Status":"0"} |
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| Start       | {"Status":"0"} |
	When connectui Input Command And Press Send
		| command_one | command_two |
		| CreateAP    | randomPwd   |
	When connectui Input Command And Press Send
		| command_one | command_two |
		| GetDevices  | null        |
	Then The response is expected
		| <expected_response>                                                              | <require_approve> | <require_verify_pwd> | <verify_whole_response> |
		| "{"ResponseCode" : "4", "Message" : "GET_DEVICES_ERROR_JSON_DESERIALIZE_ERROR"}" | false             | false                | 1                       |

@Connect
#"GetDevices"参数异常验证_空Json对象{}
Scenario: Test048_OrderId_078_"GetDevices":Empty Json Object:{}
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| CloseAP     | {"Status":"0"} |
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| Start       | {"Status":"0"} |
	When connectui Input Command And Press Send
		| command_one | command_two |
		| CreateAP    | randomPwd   |
	When connectui Input Command And Press Send
		| command_one | command_two |
		| GetDevices  | {}          |
	Then The response is expected
		| <expected_response>                                                        | <require_approve> | <require_verify_pwd> | <verify_whole_response> |
		| "{"ResponseCode" : "5", "Message" : "GET_DEVICES_ERROR_JSON_STATUS_NULL"}" | false             | false                | 1                       |

@Connect
#"GetDevices"参数异常验证_什么都不填
Scenario: Test049_OrderId_079_"GetDevices":Input Nothing
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| CloseAP     | {"Status":"0"} |
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| Start       | {"Status":"0"} |
	When connectui Input Command And Press Send
		| command_one | command_two |
		| CreateAP    | randomPwd   |
	When connectui Input Command And Press Send
		| command_one | command_two |
		| GetDevices  |             |
	Then The response is expected
		| <expected_response>                                                      | <require_approve> | <require_verify_pwd> | <verify_whole_response> |
		| "{"ResponseCode" : "2", "Message" : "GET_DEVICES_ERROR_INPUT_IS_EMPTY"}" | false             | false                | 1                       |

@Connect
#"GetDevices"参数异常验证_Status值错误
Scenario: Test050_OrderId_080_"GetDevices":The value of key "Status" is wrong
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| CloseAP     | {"Status":"0"} |
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| Start       | {"Status":"0"} |
	When connectui Input Command And Press Send
		| command_one | command_two |
		| CreateAP    | randomPwd   |
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| GetDevices  | {"Status":"1"} |
	Then The response is expected
		| <expected_response>                                                                 | <require_approve> | <require_verify_pwd> | <verify_whole_response> |
		| "{"ResponseCode" : "6", "Message" : "GET_DEVICES_ERROR_JSON_INVALID_STATUS_VALUE"}" | false             | false                | 1                       |

@Connect
#"GetDevices"参数异常验证_Status拼写错误
Scenario: Test051_OrderId_081_"GetDevices":Missing key "Status"
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| CloseAP     | {"Status":"0"} |
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| Start       | {"Status":"0"} |
	When connectui Input Command And Press Send
		| command_one | command_two |
		| CreateAP    | randomPwd   |
	When connectui Input Command And Press Send
		| command_one | command_two  |
		| GetDevices  | {"Stus":"0"} |
	Then The response is expected
		| <expected_response>                                                        | <require_approve> | <require_verify_pwd> | <verify_whole_response> |
		| "{"ResponseCode" : "5", "Message" : "GET_DEVICES_ERROR_JSON_STATUS_NULL"}" | false             | false                | 1                       |

@Connect
#"GetDevices"参数异常验证_Status拼写错误
Scenario: Test052_OrderId_082_"GetDevices":The value's type of key "Status" is wrong
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| CloseAP     | {"Status":"0"} |
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| Start       | {"Status":"0"} |
	When connectui Input Command And Press Send
		| command_one | command_two |
		| CreateAP    | randomPwd   |
	When connectui Input Command And Press Send
		| command_one | command_two  |
		| GetDevices  | {"Status":0} |
	Then The response is expected
		| <expected_response>                                                                  | <require_approve> | <require_verify_pwd> | <verify_whole_response> |
		| "{"ResponseCode" : "3", "Message" : "GET_DEVICES_ERROR_JSON_DESERIALIZE_EXCEPTION"}" | false             | false                | 1                       |

@Connect
#"IsSupportWifiDirect"参数异常验证_无效的Json对象null
Scenario: Test053_OrderId_083_"IsSupportWifiDirect":Invalid Json Object:null
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| Start       | {"Status":"0"} |
	When connectui Input Command And Press Send
		| command_one         | command_two |
		| IsSupportWifiDirect | null        |
	Then The response is expected
		| <expected_response>                                                                | <require_approve> | <require_verify_pwd> | <verify_whole_response> |
		| "{"ResponseCode" : "4", "Message" : "IS_SUPPORT_WD_ERROR_JSON_DESERIALIZE_ERROR"}" | false             | false                | 1                       |

@Connect
#"IsSupportWifiDirect"参数异常验证_空Json对象{}
Scenario: Test054_OrderId_084_"IsSupportWifiDirect":Empty Json Object:{}
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| Start       | {"Status":"0"} |
	When connectui Input Command And Press Send
		| command_one         | command_two |
		| IsSupportWifiDirect | {}          |
	Then The response is expected
		| <expected_response>                                                          | <require_approve> | <require_verify_pwd> | <verify_whole_response> |
		| "{"ResponseCode" : "5", "Message" : "IS_SUPPORT_WD_ERROR_JSON_STATUS_NULL"}" | false             | false                | 1                       |

@Connect
#"IsSupportWifiDirect"参数异常验证_什么也不填
Scenario: Test055_OrderId_085_"IsSupportWifiDirect":Input Nothing
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| Start       | {"Status":"0"} |
	When connectui Input Command And Press Send
		| command_one         | command_two |
		| IsSupportWifiDirect |             |
	Then The response is expected
		| <expected_response>                                                        | <require_approve> | <require_verify_pwd> | <verify_whole_response> |
		| "{"ResponseCode" : "2", "Message" : "IS_SUPPORT_WD_ERROR_INPUT_IS_EMPTY"}" | false             | false                | 1                       |

@Connect
#"IsSupportWifiDirect"参数异常验证_Status值错误
Scenario: Test056_OrderId_086_"IsSupportWifiDirect":The value of key "Status" is wrong
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| Start       | {"Status":"0"} |
	When connectui Input Command And Press Send
		| command_one         | command_two    |
		| IsSupportWifiDirect | {"Status":"1"} |
	Then The response is expected
		| <expected_response>                                                                   | <require_approve> | <require_verify_pwd> | <verify_whole_response> |
		| "{"ResponseCode" : "6", "Message" : "IS_SUPPORT_WD_ERROR_JSON_INVALID_STATUS_VALUE"}" | false             | false                | 1                       |

@Connect
#"IsSupportWifiDirect"参数异常验证_Status拼写错误
Scenario: Test057_OrderId_087_"IsSupportWifiDirect":Missing key "Status"
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| Start       | {"Status":"0"} |
	When connectui Input Command And Press Send
		| command_one         | command_two   |
		| IsSupportWifiDirect | {"Statu":"0"} |
	Then The response is expected
		| <expected_response>                                                          | <require_approve> | <require_verify_pwd> | <verify_whole_response> |
		| "{"ResponseCode" : "5", "Message" : "IS_SUPPORT_WD_ERROR_JSON_STATUS_NULL"}" | false             | false                | 1                       |

@Connect
#"IsSupportWifiDirect"参数异常验证_“Status”值的类型错误
Scenario: Test058_OrderId_088_"IsSupportWifiDirect":The value's type of key "Status" is wrong
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| Start       | {"Status":"0"} |
	When connectui Input Command And Press Send
		| command_one         | command_two  |
		| IsSupportWifiDirect | {"Status":0} |
	Then The response is expected
		| <expected_response>                                                                    | <require_approve> | <require_verify_pwd> | <verify_whole_response> |
		| "{"ResponseCode" : "3", "Message" : "IS_SUPPORT_WD_ERROR_JSON_DESERIALIZE_EXCEPTION"}" | false             | false                | 1                       |

@Connect
#"Start"接口依赖验证_不执行Start,执行Stop
Scenario: Test059_OrderId_054_"Not Start port":Executing the CreateAP Interface
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| Start       | {"Status":"0"} |
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| Destroy     | {"Status":"0"} |
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| Stop        | {"Status":"0"} |
	Then The response is expected
		| <expected_response>                                                 | <require_approve> | <require_verify_pwd> | <verify_whole_response> |
		| "{"ResponseCode" : "36", "Message" : "CONNECT_SMS_ERROR_UN_START"}" | false             | false                | 1                       |

@Connect
#"Start"接口依赖验证_不执行Start，执行CreateAP
Scenario: Test060_OrderId_055_"Not Start port":Executing the CreateAP Interface
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| Start       | {"Status":"0"} |
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| Destroy     | {"Status":"0"} |
	When connectui Input Command And Press Send
		| command_one | command_two                                              |
		| CreateAP    | {"SSID":"12345678","PWD":"12345678","MaxConnections":99} |
	Then The response is expected
		| <expected_response>                                                 | <require_approve> | <require_verify_pwd> | <verify_whole_response> |
		| "{"ResponseCode" : "36", "Message" : "CONNECT_SMS_ERROR_UN_START"}" | false             | false                | 1                       |

@Connect
#"Start"接口依赖验证_不执行Start，执行 CloseAP
Scenario: Test061_OrderId_056_"Not Start port":Executing the  CloseAP Interface
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| Start       | {"Status":"0"} |
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| Destroy     | {"Status":"0"} |
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| CloseAP     | {"Status":"0"} |
	Then The response is expected
		| <expected_response>                                                     | <require_approve> | <require_verify_pwd> | <verify_whole_response> |
		| "{"ResponseCode" : "38", "Message" : "CONNECT_SMS_ERROR_UN_CREATE_AP"}" | false             | false                | 1                       |

@Connect
#"Start"接口依赖验证_不执行Start，执行GetAPInfo
Scenario: Test062_OrderId_057_"Not Start port":Executing the GetAPInfo Interface
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| Start       | {"Status":"0"} |
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| Destroy     | {"Status":"0"} |
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| GetAPInfo   | {"Status":"0"} |
	Then The response is expected
		| <expected_response>                                                 | <require_approve> | <require_verify_pwd> | <verify_whole_response> |
		| "{"ResponseCode" : "36", "Message" : "CONNECT_SMS_ERROR_UN_START"}" | false             | false                | 1                       |

@Connect
#"Start"接口依赖验证_不执行Start，执行 Destroy
Scenario: Test063_OrderId_058_"Not Start port":Executing the  Destroy Interface
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| Start       | {"Status":"0"} |
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| Stop        | {"Status":"0"} |
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| Destroy     | {"Status":"0"} |
	Then The response is expected
		| <expected_response>                                                 | <require_approve> | <require_verify_pwd> | <verify_whole_response> |
		| "{"ResponseCode" : "36", "Message" : "CONNECT_SMS_ERROR_UN_START"}" | false             | false                | 1                       |

@Connect
#"Start"接口依赖验证_不执行Start，执行 IsApCreated
Scenario: Test064_OrderId_089_"Not Start port":Executing the  IsApCreated Interface
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| Start       | {"Status":"0"} |
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| Stop        | {"Status":"0"} |
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| IsApCreated | {"Status":"0"} |
	Then The response is expected
		| <expected_response>                                                 | <require_approve> | <require_verify_pwd> | <verify_whole_response> |
		| "{"ResponseCode" : "36", "Message" : "CONNECT_SMS_ERROR_UN_START"}" | false             | false                | 1                       |

@Connect
#"Start"接口依赖验证_不执行Start，执行 GetDevices
Scenario: Test065_OrderId_090_"Not Start port":Executing the  GetDevices Interface
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| Start       | {"Status":"0"} |
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| Stop        | {"Status":"0"} |
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| GetDevices  | {"Status":"0"} |
	Then The response is expected
		| <expected_response>                                                 | <require_approve> | <require_verify_pwd> | <verify_whole_response> |
		| "{"ResponseCode" : "36", "Message" : "CONNECT_SMS_ERROR_UN_START"}" | false             | false                | 1                       |

@Connect
#"Start"接口依赖验证_不执行Start，执行 IsSupportWifiDirect
Scenario: Test066_OrderId_091_"Not Start port":Executing the  IsSupportWifiDirect Interface
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| Start       | {"Status":"0"} |
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| Stop        | {"Status":"0"} |
	When connectui Input Command And Press Send
		| command_one         | command_two    |
		| IsSupportWifiDirect | {"Status":"0"} |
	Then The response is expected
		| <expected_response>                                                 | <require_approve> | <require_verify_pwd> | <verify_whole_response> |
		| "{"ResponseCode" : "36", "Message" : "CONNECT_SMS_ERROR_UN_START"}" | false             | false                | 1                       |

@Connect
#"CreateAP"接口依赖验证_不执行CreateAP，执行 CloseAP
Scenario: Test067_OrderId_059_"Not CreateAP port":Executing the  CloseAP Interface
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| Stop        | {"Status":"0"} |
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| Start       | {"Status":"0"} |
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| CloseAP     | {"Status":"0"} |
	Then The response is expected
		| <expected_response>                                                     | <require_approve> | <require_verify_pwd> | <verify_whole_response> |
		| "{"ResponseCode" : "38", "Message" : "CONNECT_SMS_ERROR_UN_CREATE_AP"}" | false             | false                | 1                       |

@Connect
#"Start"接口依赖验证_执行start、stop，再执行 IsApCreated
Scenario: Test068_OrderId_060_"Run the  start,stop port":Executing the  IsApCreated Interface
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| Start       | {"Status":"0"} |
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| Stop        | {"Status":"0"} |
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| IsApCreated | {"Status":"0"} |
	Then The response is expected
		| <expected_response>                                                 | <require_approve> | <require_verify_pwd> | <verify_whole_response> |
		| "{"ResponseCode" : "36", "Message" : "CONNECT_SMS_ERROR_UN_START"}" | false             | false                | 1                       |

@Connect
#"Start"接口依赖验证_执行start、stop，再执行 CreateAP
Scenario: Test069_OrderId_061_"Run the  start,stop port":Executing the  CreateAP Interface
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| Start       | {"Status":"0"} |
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| Stop        | {"Status":"0"} |
	When connectui Input Command And Press Send
		| command_one | command_two                                       |
		| CreateAP    | {"SSID":"1","PWD":"12345678","MaxConnections":99} |
	Then The response is expected
		| <expected_response>                                                 | <require_approve> | <require_verify_pwd> | <verify_whole_response> |
		| "{"ResponseCode" : "36", "Message" : "CONNECT_SMS_ERROR_UN_START"}" | false             | false                | 1                       |

@Connect
#"Start"接口依赖验证_执行start、stop，再执行 CloseAP
Scenario: Test070_OrderId_062_"Run the  start,stop port":Executing the  CloseAP Interface
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| Start       | {"Status":"0"} |
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| Stop        | {"Status":"0"} |
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| CloseAP     | {"Status":"0"} |
	Then The response is expected
		| <expected_response>                                                     | <require_approve> | <require_verify_pwd> | <verify_whole_response> |
		| "{"ResponseCode" : "38", "Message" : "CONNECT_SMS_ERROR_UN_CREATE_AP"}" | false             | false                | 1                       |

@Connect
#"Start"接口依赖验证_执行start、stop，再执行 GetAPInfo
Scenario: Test071_OrderId_063_"Run the  start,stop port":Executing the  GetAPInfo Interface
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| Start       | {"Status":"0"} |
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| Stop        | {"Status":"0"} |
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| GetAPInfo   | {"Status":"0"} |
	Then The response is expected
		| <expected_response>                                                 | <require_approve> | <require_verify_pwd> | <verify_whole_response> |
		| "{"ResponseCode" : "36", "Message" : "CONNECT_SMS_ERROR_UN_START"}" | false             | false                | 1                       |

@Connect
#"Start"接口依赖验证_执行start、stop，再执行 Destroy
Scenario: Test072_OrderId_064_"Run the  start,stop port":Executing the  Destroy Interface
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| Start       | {"Status":"0"} |
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| Stop        | {"Status":"0"} |
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| Destroy     | {"Status":"0"} |
	Then The response is expected
		| <expected_response>                                                 | <require_approve> | <require_verify_pwd> | <verify_whole_response> |
		| "{"ResponseCode" : "36", "Message" : "CONNECT_SMS_ERROR_UN_START"}" | false             | false                | 1                       |

@Connect
#"Start"接口依赖验证_执行start、Destroy，再执行 Start
Scenario: Test073_OrderId_065_"Run the  start,Destroy port":Executing the  Start Interface
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| Start       | {"Status":"0"} |
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| Destroy     | {"Status":"0"} |
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| Start       | {"Status":"0"} |
	Then The response is expected
		| <expected_response>                               | <require_approve> | <require_verify_pwd> | <verify_whole_response> |
		| "{"ResponseCode" : "0", "Message" : "COMMON_OK"}" | false             | false                | 1                       |
	
@Connect
#"Start"接口依赖验证_执行start、Destroy，再执行 Stop
Scenario: Test074_OrderId_066_"Run the  start,Destroy port":Executing the  Stop Interface
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| Start       | {"Status":"0"} |
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| Destroy     | {"Status":"0"} |
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| Stop        | {"Status":"0"} |
	Then The response is expected
		| <expected_response>                                                 | <require_approve> | <require_verify_pwd> | <verify_whole_response> |
		| "{"ResponseCode" : "36", "Message" : "CONNECT_SMS_ERROR_UN_START"}" | false             | false                | 1                       |

@Connect
#"Start"接口依赖验证_执行start、Destroy，再执行 IsApCreated
Scenario: Test075_OrderId_067_"Run the  start,Destroy port":Executing the  IsApCreated Interface
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| Start       | {"Status":"0"} |
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| Destroy     | {"Status":"0"} |
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| IsApCreated | {"Status":"0"} |
	Then The response is expected
		| <expected_response>                                                 | <require_approve> | <require_verify_pwd> | <verify_whole_response> |
		| "{"ResponseCode" : "36", "Message" : "CONNECT_SMS_ERROR_UN_START"}" | false             | false                | 1                       |

@Connect
#"Start"接口依赖验证_执行start、Destroy，再执行 CreateAP
Scenario: Test076_OrderId_068_"Run the  start,Destroy port":Executing the  CreateAP Interface
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| Start       | {"Status":"0"} |
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| Destroy     | {"Status":"0"} |
	When connectui Input Command And Press Send
		| command_one | command_two                                       |
		| CreateAP    | {"SSID":"1","PWD":"12345678","MaxConnections":99} |
	Then The response is expected
		| <expected_response>                                                 | <require_approve> | <require_verify_pwd> | <verify_whole_response> |
		| "{"ResponseCode" : "36", "Message" : "CONNECT_SMS_ERROR_UN_START"}" | false             | false                | 1                       |

@Connect
#"Start"接口依赖验证_执行start、Destroy，再执行 CloseAP
Scenario: Test077_OrderId_069_"Run the  start,Destroy port":Executing the  CloseAP Interface
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| Start       | {"Status":"0"} |
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| Destroy     | {"Status":"0"} |
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| CloseAP     | {"Status":"0"} |
	Then The response is expected
		| <expected_response>                                                     | <require_approve> | <require_verify_pwd> | <verify_whole_response> |
		| "{"ResponseCode" : "38", "Message" : "CONNECT_SMS_ERROR_UN_CREATE_AP"}" | false             | false                | 1                       |

@Connect
#"Start"接口依赖验证_执行start、Destroy，再执行 GetAPInfo
Scenario: Test078_OrderId_070_"Run the  start,Destroy port":Executing the  GetAPInfo Interface
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| Start       | {"Status":"0"} |
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| Destroy     | {"Status":"0"} |
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| GetAPInfo   | {"Status":"0"} |
	Then The response is expected
		| <expected_response>                                                 | <require_approve> | <require_verify_pwd> | <verify_whole_response> |
		| "{"ResponseCode" : "36", "Message" : "CONNECT_SMS_ERROR_UN_START"}" | false             | false                | 1                       |

@Connect
#"Start"接口重复性验证_重复执行start两次
Scenario: Test079_OrderId_071_"Start":Repeat "Start" twice
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| Stop        | {"Status":"0"} |
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| Start       | {"Status":"0"} |
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| Start       | {"Status":"0"} |
	Then The response is expected
		| <expected_response>                                                   | <require_approve> | <require_verify_pwd> | <verify_whole_response> |
		| "{"ResponseCode" : "32", "Message" : "CONNECT_SMS_ERROR_DUPLICAETD"}" | false             | false                | 1                       |

@Connect
#"Stop"接口重复性验证_重复执行Stop两次
Scenario: Test080_OrderId_072_"Stop":Repeat "Stop" twice
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| Start       | {"Status":"0"} |
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| Stop        | {"Status":"0"} |
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| Stop        | {"Status":"0"} |
	Then The response is expected
		| <expected_response>                                                   | <require_approve> | <require_verify_pwd> | <verify_whole_response> |
		| "{"ResponseCode" : "32", "Message" : "CONNECT_SMS_ERROR_DUPLICAETD"}" | false             | false                | 1                       |

@Connect
#"Destroy"接口重复性验证_重复执行Destroy两次
Scenario: Test081_OrderId_073_"Destroy":Repeat "Destroy" twice
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| Start       | {"Status":"0"} |
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| Destroy     | {"Status":"0"} |
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| Destroy     | {"Status":"0"} |
	Then The response is expected
		| <expected_response>                                                   | <require_approve> | <require_verify_pwd> | <verify_whole_response> |
		| "{"ResponseCode" : "32", "Message" : "CONNECT_SMS_ERROR_DUPLICAETD"}" | false             | false                | 1                       |

@Connect
#"CreateAP"接口重复性验证_重复执行CreateAP两次
Scenario: Test082_OrderId_074_"CreateAP":Repeat "CreateAP" twice
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| Start       | {"Status":"0"} |
	When connectui Input Command And Press Send
		| command_one | command_two                                       |
		| CreateAP    | {"SSID":"1","PWD":"12345678","MaxConnections":99} |
	When connectui Input Command And Press Send
		| command_one | command_two                                       |
		| CreateAP    | {"SSID":"1","PWD":"12345678","MaxConnections":99} |
	Then The response is expected
		| <expected_response>                                                   | <require_approve> | <require_verify_pwd> | <verify_whole_response> |
		| "{"ResponseCode" : "32", "Message" : "CONNECT_SMS_ERROR_DUPLICAETD"}" | false             | false                | 1                       |

@Connect
#"CloseAP"接口重复性验证_重复执行CloseAP两次
Scenario: Test083_OrderId_083_"CloseAP":Repeat "CloseAP"twice
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| Start       | {"Status":"0"} |
	When connectui Input Command And Press Send
		| command_one | command_two                                       |
		| CreateAP    | {"SSID":"1","PWD":"12345678","MaxConnections":99} |
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| CloseAP     | {"Status":"0"} |
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| CloseAP     | {"Status":"0"} |
	Then The response is expected
		| <expected_response>                                                   | <require_approve> | <require_verify_pwd> | <verify_whole_response> |
		| "{"ResponseCode" : "32", "Message" : "CONNECT_SMS_ERROR_DUPLICAETD"}" | false             | false                | 1                       |

@Connect
#"GetAPInfo"接口重复性验证_重复执行GetAPInfo两次
Scenario: Test084_OrderId_084_"GetAPInfo":Repeat "GetAPInfo"twice
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| Start       | {"Status":"0"} |
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
		| GetAPInfo   | {"Status":"0"} |
	Then The response is expected
		| <expected_response>                                                                                                                                                         | <require_approve> | <require_verify_pwd> | <verify_whole_response> |
		| "{"ResponseCode" : "0", "Message" : "GET_AP_INFO_OK", "Payload":"{"Status": 0, "Message": "WIFI_COMMON_OK", "SSID": "12345678", "PWD": "12345678", "MaxConnections": 99}"}" | false             | false                | 1                       |
	
@Connect
#"IsSupportWifiDirect"接口重复性验证_重复执行IsSupportWifiDirect两次
Scenario: Test085_OrderId_085_"IsSupportWifiDirect":Repeat "IsSupportWifiDirect"twice
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| Start       | {"Status":"0"} |
	When connectui Input Command And Press Send
		| command_one         | command_two    |
		| IsSupportWifiDirect | {"Status":"0"} |
	Then The response is expected
		| <expected_response>                                                                                                                     | <require_approve> | <require_verify_pwd> | <verify_whole_response> |
		| "{"ResponseCode" : "0", "Message" : "IS_SUPPORT_WD__OK", "Payload":"{"Status": 0, "Message": "WIFI_COMMON_OK", "WifiDirect": "True"}"}" | false             | false                | 1                       |
	When connectui Input Command And Press Send
		| command_one         | command_two    |
		| IsSupportWifiDirect | {"Status":"0"} |
	Then The response is expected
		| <expected_response>                                                                                                                     | <require_approve> | <require_verify_pwd> | <verify_whole_response> |
		| "{"ResponseCode" : "0", "Message" : "IS_SUPPORT_WD__OK", "Payload":"{"Status": 0, "Message": "WIFI_COMMON_OK", "WifiDirect": "True"}"}" | false             | false                | 1                       |

@Connect
#"GetDevices"接口重复性验证_重复执行GetDevices两次
Scenario: Test086_OrderId_086_"GetDevices":Repeat "GetDevices"twice
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
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| GetDevices  | {"Status":"0"} |
	Then The response is expected
		| <expected_response>                                                                                                                                    | <require_approve> | <require_verify_pwd> | <verify_whole_response> |
		| "{"ResponseCode" : "0", "Message" : "GET_DEVICES_OK", "Payload":"{"Status": 0, "Message": "WIFI_COMMON_OK", "Devices": "{"Count" : 0 ,"Clients" : [  ] | false             | false                | 0                       |
	
@Connect
#"IsApCreated"接口重复性验证_重复执行IsApCreated两次
Scenario: Test087_OrderId_087_"IsApCreated":Repeat "IsApCreated"twice
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
		| command_one | command_two    |
		| IsApCreated | {"Status":"0"} |		
	Then The response is expected
		| <expected_response>                                                                                                                  | <require_approve> | <require_verify_pwd> | <verify_whole_response> |
		| "{"ResponseCode" : "0", "Message" : "IS_AP_CREATED_OK", "Payload":"{"Status": 3, "Message": "WIFI_COMMON_ERROR_WIFI_NOT_RUNNING"}"}" | false             | false                | 1                       |
	#已创建AP
	When connectui Input Command And Press Send
		| command_one | command_two |
		| CreateAP    | randomPwd   |
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| IsApCreated | {"Status":"0"} |
	Then The response is expected
		| <expected_response>                                                                                              | <require_approve> | <require_verify_pwd> | <verify_whole_response> |
		| "{"ResponseCode" : "0", "Message" : "IS_AP_CREATED_OK", "Payload":"{"Status": 0, "Message": "WIFI_COMMON_OK"}"}" | false             | false                | 1                       |
	When connectui Input Command And Press Send
		| command_one | command_two    |
		| IsApCreated | {"Status":"0"} |
	Then The response is expected
		| <expected_response>                                                                                              | <require_approve> | <require_verify_pwd> | <verify_whole_response> |
		| "{"ResponseCode" : "0", "Message" : "IS_AP_CREATED_OK", "Payload":"{"Status": 0, "Message": "WIFI_COMMON_OK"}"}" | false             | false                | 1                       |


@Connect
#connect ui stop button
Scenario: Turn Off 0_close_connect_button_1
	When Click Stop Button on The window_name
		| window_name |
		| connect     |
@Connect
#关闭connect窗口
Scenario: Turn Off 1_close_connect_1
	When close connect or discover UI
		| window_name |
		| connect     |
@Connect
#关闭主窗口
Scenario: Turn Off 2_close_unity_1
	When close connect or discover UI
		| window_name |
		| unity       |