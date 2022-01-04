
Feature: Product Feature
@Local_Parallel
  Scenario: Apply Apple Vendor Filter
    Given I navigate to website locally
    And I press the Apple Vendor Filter
    Then I should see 9 items in the list

 @Local_Parallel
  Scenario: Apply Lowest to Highest Order By
    Given I navigate to website locally
    And I order by lowest to highest
    Then I should see prices in ascending order