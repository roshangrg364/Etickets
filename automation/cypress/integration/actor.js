///<reference types = "cypress" />
import { performLogin, generatename } from "./config.js";
describe("Login ", () => {
  Cypress.Cookies.defaults({
    preserve: ".AspNetCore.Identity.Application",
  });
  before(function () {
    cy.visit("Actor/Index");
    cy.login();
    //.AspNetCore.Identity.Application
  });
  beforeEach(function () {
    cy.contains("Management").click();
    cy.contains("Actor").click();
    cy.contains("Add").click();
  });
  it.only("actor is created with correct data", () => {
    const actorName = generatename();
    cy.get("#file").attachFile("benedict.jpg");
    cy.get("#Name").type(actorName);
    cy.get("#Description").type("dr strange");
    cy.contains("Create").click();
    cy.url().should("eq", "https://localhost:44319/Actor/Index");
  });

  it.only("actor is created with correct data", () => {
    // cy.contains("Management").click();
    // cy.contains("Actor").click();
    // cy.contains("Add").click();
    const actorName = generatename();
    cy.get("#file").attachFile("benedict.jpg");
    cy.get("#Name").type(actorName);
    cy.get("#Description").type("dr strange");
    cy.contains("Create").click();
    cy.url().should("eq", "https://localhost:44319/Actor/Index");
  });

  it.only("actor is created with correct data", () => {
    // cy.contains("Management").click();
    // cy.contains("Actor").click();
    // cy.contains("Add").click();
    const actorName = generatename();
    cy.get("#file").attachFile("benedict.jpg");
    cy.get("#Name").type(actorName);
    cy.get("#Description").type("dr strange");
    cy.contains("Create").click();
    cy.url().should("eq", "https://localhost:44319/Actor/Index");
  });
});
