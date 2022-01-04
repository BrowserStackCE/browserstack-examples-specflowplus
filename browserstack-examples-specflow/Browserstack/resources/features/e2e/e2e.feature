@Parallel
Feature: e2e Feature

  Scenario: End to End Scenario
    Given I navigate to website
    Then I click on Sign In link
    When I type "fav_user" in username
    When I type "testingisfun99" in password
    Then I press Log In Button
    Then I add two products to cart
    Then I click on Buy Button
    When I type "first" in firstNameInput input
    When I type "last" in lastNameInput input
    When I type "test address" in addressLineInput input
    When I type "test province" in provinceInput input
    When I type "123456" in postCodeInput input
    Then I click on Checkout Button
    Then I click on "Orders" link
    Then I should see elements in list
