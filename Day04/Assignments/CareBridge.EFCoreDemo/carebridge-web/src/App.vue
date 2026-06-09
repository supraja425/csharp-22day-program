<template>
  <div>
    <h2>Department Load Analytics</h2>

    <table border="1" width="100">
      <thead>
        <tr>
          <th>Department</th>
          <th>Inpatient</th>
          <th>Outpatient</th>
          <th>ED</th>
          <th>Total</th>
        </tr>
      </thead>
      <tbody>
        <tr
          v-for="(dept, index) in departments"
          :key="dept.departmentName"
          :class="{ highlight: index === 0 }"
        >
          <td>{{ dept.departmentName }}</td>
          <td>{{ dept.inpatient }}</td>
          <td>{{ dept.outpatient }}</td>
          <td>{{ dept.ed }}</td>
          <td><b>{{ dept.total }}</b></td>
        </tr>
        
        <tr class="grand-total">
          <td><b>Grand Total</b></td>
          <td>{{ grandTotal.inpatient }}</td>
          <td>{{ grandTotal.outpatient }}</td>
          <td>{{ grandTotal.ed }}</td>
          <td><b>{{ grandTotal.total }}</b></td>
        </tr>

      </tbody>
    </table>
  </div>
</template>

<script>
import { computed } from 'vue';
export default {
  data() {
    return {
      departments: [],
      grandTotal: {}
    };
  },
  mounted() {
    fetch("https://localhost:7079/api/analytics/department-load")
      .then(res => res.json())
      .then(data => {
        this.departments = data.departments;
        this.grandTotal = data.grandTotal;
      });
  }
};


</script>

<style>
.highlight {
  background-color: yellowgreen;
  font-weight: bold;
}
</style>