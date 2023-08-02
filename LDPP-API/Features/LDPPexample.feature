Feature: LDPPexample

A short summary of the feature


Scenario Template: test0_open_ldpp_exe
	Given Open Ldpp App Window <exe_path>
	And Add Waiting Time <sleep_time1>
	When Click Sign In Button
Scenarios:
| exe_path                                                                                                                                                           | sleep_time1 | sleep_time2 |
| C:\\API\\unityLDPP\\Windows\\agent\\UnitySampleAppWindows\\bin\\x64\\Debug\\net6.0-windows10.0.19041\\UnitySampleAppWindows.exe | 500         | 30000       |

Scenario: aTest_Init_Exe
	When start exe and init exe 
	| device_name | exe_path    | 
	| 697       | C:\\API\\unityLDPP\\Windows\\agent\\UnitySampleAppWindows\\bin\\x64\\Debug\\net6.0-windows10.0.19041\\UnitySampleAppWindows.exe |                


Scenario: bIs_Open_LdppPlugin
	When init done 
	| expected_response | 
	| true       | 

Scenario: cTest_Connect_Start
	When start discover device 
	| expected_response | 
	| true       | 

Scenario: dIsDiscoverDevic
	When start done 
	| expected_response | 
	| true       | 

#     
Scenario: eTest_Connect_CreateAP
	When start connect device 
	| expected_response | 
	| true       | 

Scenario: eTest_Disconnect_AP
	When start Disconnect device 
	| expected_response | 
	| true       | 

Scenario: fTest_CloseAllWindows
	When close all windows 
	| expected_response | 
	| true       | 
