class MySidebar extends HTMLElement {
  connectedCallback() {
    this.innerHTML = `
    <aside>

      <div class="top">

        <div class="logo">
          <img src="/img/logo.png">
          <h3>COMPANY</h3>
        </div>

        <div class="close" id="close-btn">
          <span class="material-icons-outlined">close</span>
        </div>

      </div>



      <div class="sidebar">

        <a href="#" class="active">
          <span class="material-icons-outlined">dashboard</span>
          <h3>Dashboard</h3>
        </a>

        <a href="/profile.html">
          <span class="material-icons-outlined">person_outline</span>
          <h3>Profile</h3>
        </a>

        <a href="#">
          <span class="material-icons-outlined">account_tree</span>
          <h3>Project</h3>
        </a>

        <a href="#">
          <span class="material-icons-outlined">view_list</span>
          <h3>Manage</h3>
        </a>

        <a href="#">
          <span class="material-icons-outlined">bar_chart</span>
          <h3>Statistics</h3>
        </a>

        <a href="#">
          <span class="material-icons-outlined">insert_invitation</span>
          <h3>Calendar</h3>
        </a>

        <a href="#">
          <span class="material-icons-outlined">groups</span>
          <h3>Groups</h3>
        </a>

        <a href="#">
          <span class="material-icons-outlined">currency_exchange</span>
          <h3>Payment</h3>
        </a>

        <a href="/login.html">
          <span class="material-icons-outlined">logout</span>
          <h3>Logout</h3>
        </a>

      </div>

    </aside>
    `;
  }
}
    
customElements.define('my-sidebar', MySidebar)