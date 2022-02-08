![Logo](https://www.browserstack.com/images/static/header-logo.jpg)

# BrowserStack Examples SpecflowPlus <a href="https://specflow.org/"><img src="https://www.specflow.org/wp-content/uploads/2016/07/SF_Logo.png" alt="Specflow" height="22" alt="Behavior Driven Development for .NET" /></a>

## Introduction

SpecFlow is the #1 .NET open source framework for Behavior Driven Development, Acceptance Test Driven Development and Specification by Example. With over 10m downloads on NuGet, SpecFlow is trusted by teams around the world.

This BrowserStack Example repository demonstrates a #{ Selenium test / Cypress / Puppeteer / Other } framework written in Cucumber and Junit 5 with parallel testing capabilities. The #{ Selenium test / Cypress / Puppeteer / Other } test scripts are written for the open source [BrowserStack Demo web application](https://bstackdemo.com) ([Github](https://github.com/browserstack/browserstack-demo-app)). This BrowserStack Demo App is an e-commerce web application which showcases multiple real-world user scenarios. The app is bundled with offers data, orders data and products data that contains everything you need to start using the app and run tests out-of-the-box.

The #{ Selenium test / Cypress / Puppeteer / Other } tests are run on different platforms like on-prem, docker and BrowserStack using various run configurations and test capabilities.

---

## Repository setup

-   Clone the repository

-   Ensure you have the following dependencies installed on the machine
    -.Net Core >= 3.1
    -Visual Studio 2019

    .Net Core:

    ```
    dotnet restore
    ```

## About the tests in this repository

This repository contains the following #{ Selenium test} tests:

| Module  | Test name                          | Description                                                                                                                                                                                                      |
| ------- | ---------------------------------- | ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| E2E     | End to End Scenario                | This test scenario verifies successful product purchase lifecycle end-to-end. It is executed in Parallel profile.                                                                                                |
| Login   | Login with given username          | This test verifies the login workflow with different types of valid login users.It is executed in Single profile.                                                                                                |
| Login   | Login as Locked User               | This test verifies the login workflow error for a locked user. It is executed in Single profile.                                                                                                                 |
| Offers  | Offers for Mumbai location         | This test mocks the GPS location for Mumbai and verifies that the product offers applicable for the Mumbai location are shown. It is executed in Local profile.                                                  |
| Product | Apply Apple Vendor Filter          | This test verifies that the Apple products are only shown if the Apple vendor filter option is applied. It is executed in Local_Parallel profile.                                                                |
| Product | Apply Lowest to Highest Order By   | This test verifies that the product prices are in ascending order when the product sort "Lowest to Highest" is applied. It is executed in Local_Parallel profile.                                                |
| User    | Login as User with no image loaded | This test verifies that the product images load for user: "image_not_loading_user" on the e-commerce application. Since the images do not load, the test case assertion fails. It is executed in Mobile profile. |
| User    | Login as User with existing Orders | This test verifies that existing orders are shown for user: "existing_orders_user" .It is executed in Mobile profile.                                                                                            |

---

## Test infrastructure environments

-   [ON_PREM](#on-premise-self-hosted)
-   [DOCKER](#docker)
-   [default/null/""](#browserstack)

---

# On Premise / Self Hosted

This infrastructure points to running the tests on your own machine using a browser (e.g. Chrome) using the browser's driver executables (e.g. ChromeDriver for Chrome). #{ Selenium enables this functionality using WebDriver for many popular browsers.}

## Prerequisites

-   For this infrastructure configuration (i.e on-premise), ensure that the ChromeDriver executable is placed in the `SpecflowBrowserStack/Drivers/OnPremDriver/` folder.
-   ChromeDriver can be downloaded from https://chromedriver.chromium.org/downloads

Note: The ChromeDriver version must match the Chrome browser version on your machine.

## Running Your Tests

### Run a specific test on your own machine

-   How to run the test?

    To run the default test scenario (e.g. Login Scenario) on your own machine, use the following command:

    .Net Core:

    ```
    set TEST_INFRA=ON_PREM
    dotnet test --filter "TestCategory=Single"
    ```

    To run a specific test scenario use the filter tagged to that feature file.

    .Net Core:

    ```
    dotnet test --filter "TestCategory=<Tag>"
    ```

    where, the argument 'Tag' can be any profile configured with filters in feature files for this repository.

    E.g. "Single", "Local".

-   Output

    This run profile executes a specific test scenario on a single browser instance on your own machine.

-   Output

    This run profile executes the test Feature file sequentially on a single browser, on your own machine.

---

# Docker

[Docker](https://docs.docker.com/get-started/overview/) is an open source platform that provides the ability to package and test applications in an isolated environment called containers.

## Prerequisites

-   Install and start [Docker](https://docs.docker.com/get-docker/).
-   Note: Docker should be running on the test machine. Ensure Docker Compose is installed as well.
-   Run `docker-compose pull` from the current directory of the repository.

## Running Your Tests

### Run a specific test on the docker infrastructure

-   How to run the test?

    -   Start the Docker by running the following command:

    ```
    docker-compose up -d
    ```

    To run the default test scenario (e.g. Login Scenario) on your own machine, use the following command:

    .Net Core:

    ```
    set TEST_INFRA=DOCKER
    dotnet test --filter "TestCategory=Single"
    ```

    To run a specific test scenario use the filter tagged to that feature file.

    .Net Core:

    ```
    dotnet test --filter "TestCategory=<Tag>"
    ```

    where, the argument 'Tag' can be any profile configured with filters in feature files for this repository.

    E.g. "Single", "Local".

-   Output

    This run profile executes a specific test scenario on a single browser instance on your own machine.

-   Output

    This run profile executes the test Feature file sequentially on a single browser, on your own machine.

    -   After tests are complete, you can stop the Docker by running the following command:

    ```
    docker-compose down
    ```

-   Output

    This run profile executes a specific test scenario on a single browser deployed on a docker image.

---

# BrowserStack

[BrowserStack](https://browserstack.com) provides instant access to 2,000+ real mobile devices and browsers on a highly reliable cloud infrastructure that effortlessly scales as testing needs grow.

## Prerequisites

-   Create a new [BrowserStack account](https://www.browserstack.com/users/sign_up) or use an existing one.
-   Identify your BrowserStack username and access key from the [BrowserStack Automate Dashboard](https://automate.browserstack.com/) and export them as environment variables using the below commands.

    -   For \*nix based and Mac machines:

    ```sh
    export BROWSERSTACK_USERNAME=<browserstack-username> &&
    export BROWSERSTACK_ACCESS_KEY=<browserstack-access-key>
    ```

    -   For Windows:

    ```shell
    set BROWSERSTACK_USERNAME=<browserstack-username>
    set BROWSERSTACK_ACCESS_KEY=<browserstack-access-key>
    ```

    Alternatively, you can also hardcode username and access_key objects in the [conf.json](SpecflowBrowserStack\conf.json) file.

Note:

-   We have configured a list of test capabilities in the [conf.json](SpecflowBrowserStack\conf.json) file. You can certainly update them based on your device / browser test requirements.
-   The exact test capability values can be easily identified using the [Browserstack Capability Generator](https://browserstack.com/automate/capabilities)

## Running Your Tests

### Run a specific test on BrowserStack

In this section, we will run a single test on Chrome browser on Browserstack. To change test capabilities for this configuration, please refer to the `single` object in `conf.json` file.

-   How to run the test?

    -   To run the default test scenario (e.g. Login Scenario) on your own machine, use the following command:

    .Net Core:

    ```
    dotnet test --filter "TestCategory=Single"
    ```

    To run a specific test scenario use the filter tagged to that feature file.

    ```
    dotnet test --filter "TestCategory=<Tag>"
    ```

    where,the argument 'Tag' can be any profile configured with filters in feature files for this repository.

    E.g. Login Feature can be run by Tag "Single", Offer Feature can be run by Tag "Local",Product Feature can be run by Tag "Local_Parallel" ,User Feature can be run by Tag "Mobile",E2E Feature can be run by Tag "Parallel"[About the tests in this repository](#About-the-tests-in-this-repository) section.

-   Output

    This run profile executes a single/local/mobile/parallel/local_parallel test on a single/multiple browser on BrowserStack. Please refer to your [BrowserStack dashboard](https://automate.browserstack.com/) for test results.

### [Web application hosted on internal environment] Running your tests on BrowserStack using BrowserStackLocal

#### Prerequisites

-   Clone the [BrowserStack demo application](https://github.com/browserstack/browserstack-demo-app) repository.
    ```
    git clone https://github.com/browserstack/browserstack-demo-app
    ```
-   Please follow the README.md on the BrowserStack demo application repository to install and start the dev server on localhost.
-   In this section, we will run a single test case to test the BrowserStack Demo app hosted on your local machine i.e. localhost. Refer to the `Local` object in `conf.json` file to change test capabilities for this configuration.
-   Note: You may need to provide additional BrowserStackLocal arguments to successfully connect your localhost environment with BrowserStack infrastructure. (e.g if you are behind firewalls, proxy or VPN).
-   Further details for successfully creating a BrowserStackLocal connection can be found here:

    -   [Local Testing with Automate](https://www.browserstack.com/local-testing/automate)
    -   [BrowserStackLocal C# GitHub](https://github.com/browserstack/browserstack-local-csharp)
    -   Onces Connection is established user "browserstack.local": "true" in cpabalitilies.

### [Web application hosted on internal environment] Run a specific test on BrowserStack using BrowserStackLocal

-   How to run the test?

    -Product Feature can be run by Tag "Local" on a single BrowserStack browser using BrowserStackLocal, use the following command:

    .Net Core:

    ```
    dotnet test --filter "TestCategory=Local"
    ```

-   Output

    This run profile executes a single test on an internally hosted web application on a single browser on BrowserStack. Please refer to your BrowserStack dashboard(https://automate.browserstack.com/) for test results.

## Additional Resources

-   View your test results on the [BrowserStack Automate dashboard](https://www.browserstack.com/automate)
-   Documentation for writing [Automate test scripts in C#](https://www.browserstack.com/docs/automate/selenium/getting-started/c-sharp)
-   Customizing your tests capabilities on BrowserStack using our [test capability generator](https://www.browserstack.com/automate/capabilities)
-   [List of Browsers & mobile devices](https://www.browserstack.com/list-of-browsers-and-platforms?product=automate) for automation testing on BrowserStack #{ Replace link for non-Selenium frameworks. }
-   [Using Automate REST API](https://www.browserstack.com/automate/rest-api) to access information about your tests via the command-line interface
-   Understand how many parallel sessions you need by using our [Parallel Test Calculator](https://www.browserstack.com/automate/parallel-calculator?ref=github)
-   For testing public web applications behind IP restriction, [Inbound IP Whitelisting](https://www.browserstack.com/local-testing/inbound-ip-whitelisting) can be enabled with the [BrowserStack Enterprise](https://www.browserstack.com/enterprise) offering

## Run the entire test suite in parallel on a single BrowserStack browser

In this section, we will run the tests in parallel on a single browser on Browserstack. Refer to single object in caps.json file.

How to run the test?

To run the entire test suite in parallel on a single BrowserStack browser, update Tags in all Feature file as @Single and use the following command:

```
  dotnet test --filter "TestCategory=Single" or dotnet test.
```

#Output

    This run profile executes the entire test suite in parallel on a single BrowserStack browser. Please refer to your [BrowserStack dashboard](https://automate.browserstack.com/) for test results.

      Note:By Default Login Feature is executed on Single profile,Offer Feature is executed on Local profile,E2E Feature is executed on Parallel profile,User Feature is executed on Mobile profile,Product Feature is executed on Local_Parallel profile.

## Run the entire test suite in parallel on a multiple BrowserStack browser

In this section, we will run the tests in parallel on a single browser on Browserstack. Refer to single object in caps.json file.

How to run the test?

To run the entire test suite in parallel on a multiple BrowserStack browser, update Tags in all Feature file as @Parallel and use the following command:

```
  dotnet test --filter "TestCategory=Parallel" or dotnet test.
```

#Output

    This run profile executes the entire test suite in parallel on a multiple BrowserStack browser. Please refer to your [BrowserStack dashboard](https://automate.browserstack.com/) for test results.

      Note:By Default Login Feature is executed on Single profile,Offer Feature is executed on Local profile,E2E Feature is executed on Parallel profile,User Feature is executed on Mobile profile,Product Feature is executed on Local_Parallel profile.

## Observations

-If Test are skipped, please check for other instances of .Net Host & BrowserstackLocal running in background and terminate the running instances explicity.

## Open Issues

-When running all the tests together, there is some flakiness observed and some Test might get fail.
