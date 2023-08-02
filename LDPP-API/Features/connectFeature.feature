#Feature: connectFeature
#
#A short summary of the feature
#
#Scenario Template: test0_open_ldpp_exe
#	Given Open Ldpp App Window <exe_path>
#	And Add Waiting Time <sleep_time1>
#	When Click Sign In Button
#Scenarios:
#| exe_path                                                        | sleep_time1 | sleep_time2 |
#| C:\\DCLDPP\\net6.0-windows10.0.19041\\UnitySampleAppWindows.exe | 500         | 30000       |
#
#Scenario Template:test1_open_window_and_click_start_button
#	When Choose <window_name> from The Following Windows Options And Click Test Button 
#	When Click Start Button on The <window_name> Window
#	Then The response is expected <expected_response> in <require_approve> under <require_verify_pwd> after <verify_whole_response> 
#Scenarios:
#| window_name | expected_response                         | require_approve | require_verify_pwd | verify_whole_response |
#| connect     | "plugin \| Plugin started successfully!!" | true            | false              | 1                     |
#
#@tag1
##账号密码随机的需要写 randomPwd ${SSID} ${PWD}
##需要验证账号密码的 require_verify_pwd 字段
#Scenario Template: test2_start_test
#	When Input Command And Press Send <command_one> <command_two> from <windows_name>
#	Then The response is expected <expected_response> in <require_approve> under <require_verify_pwd> after <verify_whole_response>
#Scenarios:
#| command_one | command_two    | expected_response | <windows_name> | require_approve | require_verify_pwd | verify_whole_response |
##| Start       | {"Status":"0"} | "{"ResponseCode" : "0", "Message" : "COMMON_OK"}"                                                                                                  | connect        | true            | false              | 1                     |
#| CreateAP    | randomPwd      |                   | connect        | true            | true               | 1                     |
##| IsApCreated | {"Status":"0"} | "{"ResponseCode" : "0", "Message" : "IS_AP_CREATED_OK", "Payload":"{"Status": 0, "Message": "WIFI_COMMON_OK"}"}"                                   | connect        | true            | false              | 1                     |
##| GetAPInfo   | {"Status":"0"} | "{"ResponseCode" : "0", "Message" : "GET_AP_INFO_OK", "Payload":"{"Status": 0, "Message": "WIFI_COMMON_OK", "SSID": "${SSID}", "PWD": "${PWD}"}"}" | connect        | true            | true               | 1                     |
##| GetDevices  | {"Status":"0"} | "Message" : "GET_DEVICES_OK","Status": 0                                                                                                           | connect        | true            | false              | 0                     |
##| CloseAP     | {"Status":"0"} | "{"ResponseCode" : "0", "Message" : "CLOSE_AP_OK", "Payload":"{"Status": 0, "Message": "WIFI_STOP_OK"}"}"                                          | connect        | true            | false              | 1                     |
#| Start       | {"Status":"0"} |                   | discover       | true            | false              | 1                     |
#
#Scenario Template: test3_click_stop_button
#	When Click Stop Button on The <window_name> Window
#	Then The response is expected <expected_response> in <require_approve> under <require_verify_pwd> after <verify_whole_response>
#Scenarios:
#| window_name | expected_response                         | require_approve | require_verify_pwd | verify_whole_response |
#| discover    | "plugin \| Plugin stopped successfully!!" | true            | false              | 1                     |
#
#Scenario Template: test3_click_sign_out_button
#	When Click Sign out Button
#
