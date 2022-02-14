@user
Feature: User Feature

  Scenario: Login as User with no image loaded
    Given I navigate to website
    Then I click on Sign In link
    When I type "image_not_loading_user" in username
    When I type "testingisfun99" in password
    Then I press Log In Button
    Then I should see no image loaded

  Scenario: Login as User with existing Orders
    Given I navigate to website
    Then I click on Sign In link
    When I type "existing_orders_user" in username
    When I type "testingisfun99" in password
    Then I press Log In Button
    Then I click on "Orders" link
    Then I should see elements in list
