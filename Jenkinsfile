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
					cp -r browserstack_examples_specflowplus/BrowserStack/Webdriver/Resources/* browserstack_examples_specflowplus/bin/Debug/netcoreapp3.1/BrowserStack/Webdriver/Resources/
					/usr/local/bin/dotnet build
				'''
			
				if(TEST_TYPE == "single"){
					sh returnStatus:true,script: '''
						/usr/local/bin/dotnet test --filter Category=single
					'''
				} else if(TEST_TYPE == "single-local") {
					sh returnStatus:true,script: '''
						export CAPABILITIES_FILENAME=capabilities-local.yml
						/usr/local/bin/dotnet test --filter Category=single
					'''
				} else if(TEST_TYPE == "parallel-local"){
					sh returnStatus:true,script: '''
						export CAPABILITIES_FILENAME=capabilities-local.yml
						/usr/local/bin/dotnet test
					'''
				} else {
					sh returnStatus:true,script: '''
						/usr/local/bin/dotnet test
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
