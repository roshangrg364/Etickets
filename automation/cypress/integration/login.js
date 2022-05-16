///<reference types = "cypress" />
import { performLogin } from "./config.js";
describe("Login ", () => {
  let loginData;
  beforeEach(function () {
    cy.visit("https://localhost:44319/Account/Login");
    loginData = this.loginData;
  });
  it.only("cannot login without username and password", () => {
    cy.get("#UserName").clear();
    cy.get("#Password").clear();
    cy.contains("Login").click();
    cy.get("#username-msg").should("have.text", "Username is required");
    cy.get("#password-msg").should("have.text", "Password is required");
    cy.url().should("eq", "https://localhost:44319/Account/Login");
  });

  it.only("User is logged in with correct credentials", () => {
    cy.login(Cypress.env("Username"), Cypress.env("Password"));
    cy.url().should("eq", "https://localhost:44319/Home/Index");
  });

  it.only("cannot login with incorrect password", () => {
    cy.get("#UserName").type("admin");
    cy.get("#Password").type("admin13");
    cy.contains("Login").click();

    cy.url().should("eq", "https://localhost:44319/Account/Login");
    cy.get("#swal2-html-container").should(
      "have.text",
      "Error: Incorrect username or password"
    );
  });
});
