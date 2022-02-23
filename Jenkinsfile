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

		stage('Pull from Github') {
			dir('test') {
				git branch: 'nunit_runner_development', changelog: false, poll: false, url: 'https://github.com/browserstack/browserstack-examples-specflowplus.git'
			}
		}

		stage('Run Test(s)') {
			browserstack(credentialsId: "${params.BROWSERSTACK_USERNAME}") {
				if(TEST_TYPE == "single"){
					sh returnStatus:true,script: '''
						cd test/browserstack_examples_specflowplus
						/usr/local/bin/dotnet build
						/usr/local/bin/dotnet restore
						/usr/local/bin/dotnet test --filter Category=single
					'''
				} else if(TEST_TYPE == "single-local") {
					sh returnStatus:true,script: '''
						cd test/browserstack_examples_specflowplus
						export CAPABILITIES_FILENAME=capabilities-local.yml
						/usr/local/bin/dotnet build
						/usr/local/bin/dotnet restore
						/usr/local/bin/dotnet test --filter Category=single
					'''
				} else if(TEST_TYPE == "parallel-local"){
					sh returnStatus:true,script: '''
						cd test/browserstack_examples_specflowplus
						export CAPABILITIES_FILENAME=capabilities-local.yml
						/usr/local/bin/dotnet build
						/usr/local/bin/dotnet restore
						/usr/local/bin/dotnet test
					'''
				} else {
					sh returnStatus:true,script: '''
						cd test/browserstack_examples_specflowplus
						/usr/local/bin/dotnet build
						/usr/local/bin/dotnet restore
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
