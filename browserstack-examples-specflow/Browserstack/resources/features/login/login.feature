@Single
Feature: Login Feature

  Scenario Outline: Login as <username>
    Given I navigate to website
    Then I click on Sign In link
    When I type <username> in username
    When I type <password> in password
    Then I press Log In Button
    Then I should see user <username> logged in
    Examples:
      | username                 | password         |
      | 'fav_user'               | 'testingisfun99' |
      | 'image_not_loading_user' | 'testingisfun99' |
      | 'existing_orders_user'   | 'testingisfun99' |
      | 'locked_user'            | 'testingisfun99' |
 