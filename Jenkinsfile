import org.jenkinsci.plugins.pipeline.modeldefinition.Utils

node {
	try {
		properties([
			parameters([
				credentials(credentialType: 'com.browserstack.automate.ci.jenkins.BrowserStackCredentials', defaultValue: '', description: 'Select your BrowserStack Username', name: 'BROWSERSTACK_USERNAME', required: true),
				[$class: 'ExtensibleChoiceParameterDefinition',
				choiceListProvider: [
					$class: 'TextareaChoiceListProvider',
					addEditedValue: false,
					choiceListText: '''single
parallel
single-local
parallel-local
''',
					defaultChoice: 'parallel'
				],
				description: 'Select the test you would like to run',
				editable: false,
				name: 'TEST_TYPE']
			])
		])

		stage('Setup') {
			cleanWs()
			checkout scm
		}

		stage('Run Test(s)') {
			browserstack(credentialsId: "${params.BROWSERSTACK_USERNAME}") {
				sh returnStatus:true, script:'''
					mkdir -p browserstack_examples_specflowplus/bin/Debug/netcoreapp3.1/BrowserStack/Webdriver/Resources
					echo  \'\'\'DriverType: CloudDriver
BaseUrl: http://localhost:3000
CloudDriverConfig:
  HubUrl: https://hub-cloud.browserstack.com/wd/hub
  User:
  Key:
  LocalTunnel:
    IsEnabled: true
    LocalOptions:
      binarypath: ~/.browserstack/BrowserStackLocal
  CommonCapabilities:
    BStackOptions:
      projectName: BrowserStack Examples Specflow
      buildName: browserstack-examples-specflow
      debug: true
      networkLogs: true
      os: Windows
      osVersion: "11"
      local: true
  Platforms:
    - SessionCapabilities:
        PlatformOptions: 
          BrowserVersion: latest
\'\'\'	> browserstack_examples_specflowplus/BrowserStack/Webdriver/Resources/capabilities-local.yml
					cp -r browserstack_examples_specflowplus/BrowserStack/Webdriver/Resources/* browserstack_examples_specflowplus/bin/Debug/netcoreapp3.1/BrowserStack/Webdriver/Resources/
					/bin/dotnet build
				'''
			
				if(TEST_TYPE == "single"){
					sh returnStatus:true,script: '''
						/bin/dotnet test --filter Category=single
					'''
				} else if(TEST_TYPE == "single-local") {
					sh returnStatus:true,script: '''
						export CAPABILITIES_FILENAME=capabilities-local.yml
						/bin/dotnet test --filter Category=single
					'''
				} else if(TEST_TYPE == "parallel-local"){
					sh returnStatus:true,script: '''
						export CAPABILITIES_FILENAME=capabilities-local.yml
						/bin/dotnet test
					'''
				} else {
					sh returnStatus:true,script: '''
						/bin/dotnet test
					'''
				}
			}
		}
	} catch (e) {
		currentBuild.result = 'FAILURE'
		throw e
	} finally {
		stage('Publish Results'){
			browserStackReportPublisher 'automate'
		}
	}
}
