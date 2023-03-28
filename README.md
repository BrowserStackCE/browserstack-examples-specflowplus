![Logo](https://www.browserstack.com/images/static/header-logo.jpg)

# BrowserStack Examples Specflow <a href="https://specflow.org/"><img src="https://www.specflow.org/wp-content/uploads/2016/07/SF_Logo.png" alt="Specflow" height="22" alt="Behavior Driven Development for .NET" /></a>

## Introduction

SpecFlow is an open source framework for Behavior Driven Development, Acceptance Test Driven Development and Specification by Example.

This BrowserStack Example repository demonstrates a Selenium test framework written in Cucumber and NUnit with parallel testing capabilities. The Selenium test scripts are written for the open source [BrowserStack Demo web application](https://bstackdemo.com) ([Github](https://github.com/browserstack/browserstack-demo-app)). This BrowserStack Demo App is an e-commerce web application which showcases multiple real-world user scenarios. The app is bundled with offers data, orders data and products data that contains everything you need to start using the app and run tests out-of-the-box.

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

    Alternatively, you can also hardcode username and access_key objects in the [browserstack.yml](browserstack_examples_specflowplus/browserstack.yml) file.

Note:

-   The exact test capability values can be easily identified using the [Browserstack Capability Generator](https://browserstack.com/automate/capabilities)

## Running Your Tests

In this section, we will run tests on Browserstack. To change test capabilities for this configuration, please refer to the `platforms` object in `browserstack.yml` file.

-   How to run the test?

    To run a test scenario (e.g. Login Scenario) on your own machine, use the following command:

    .Net Core:

    ```
    dotnet test --filter Category=single
    ```

    To run a specific test scenario use the filter tagged to that feature file.

    ```
    dotnet test --filter Category=<Tag>
    ```

    where,the argument 'Tag' can be any profile configured with filters in feature files for this repository.

    E.g. "single", "e2e", "login", "user", "offers" and "product"

    ---

    To run all the tests, use the following command:

    .Net Core:

    ```
    dotnet test
    ```

    The number of prallels per platform can be configured by changing the value of `parallelsPerPlatform` property in the `browserstack.yml` file.

-   Output

    This run profile executes a single test on a single browser on BrowserStack. Please refer to your [BrowserStack dashboard](https://automate.browserstack.com/) for test results.

## Additional Resources

-   View your test results on the [BrowserStack Automate dashboard](https://www.browserstack.com/automate)
-   Documentation for writing [Automate test scripts in C#](https://www.browserstack.com/docs/automate/selenium/getting-started/c-sharp)
-   Customizing your tests capabilities on BrowserStack using our [test capability generator](https://www.browserstack.com/automate/capabilities)
-   [List of Browsers & mobile devices](https://www.browserstack.com/list-of-browsers-and-platforms?product=automate) for automation testing on BrowserStack #{ Replace link for non-Selenium frameworks. }
-   [Using Automate REST API](https://www.browserstack.com/automate/rest-api) to access information about your tests via the command-line interface
-   Understand how many parallel sessions you need by using our [Parallel Test Calculator](https://www.browserstack.com/automate/parallel-calculator?ref=github)
-   For testing public web applications behind IP restriction, [Inbound IP Whitelisting](https://www.browserstack.com/local-testing/inbound-ip-whitelisting) can be enabled with the [BrowserStack Enterprise](https://www.browserstack.com/enterprise) offering