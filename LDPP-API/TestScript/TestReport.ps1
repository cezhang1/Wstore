#######################################
##Allure 报告本地查看，请注意以下3项 ##
##1、本机配置了Allure 插件           ##
##2、Allure 配置了环境变量           ##
##3、Allure 报告json文件已准备好     ##
#######################################
cd ..
write-host "generate allure html report"
allure generate ./allure-results -o ./allure-results/html-report --clean
write-host "waiting 5s"
#Start-Sleep -s 5
write-host "open allure html report"
allure open ./allure-results/html-report