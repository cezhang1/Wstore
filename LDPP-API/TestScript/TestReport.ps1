#######################################
##Allure ���汾�ز鿴����ע������3�� ##
##1������������Allure ���           ##
##2��Allure �����˻�������           ##
##3��Allure ����json�ļ���׼����     ##
#######################################
cd ..
write-host "generate allure html report"
allure generate ./allure-results -o ./allure-results/html-report --clean
write-host "waiting 5s"
#Start-Sleep -s 5
write-host "open allure html report"
allure open ./allure-results/html-report