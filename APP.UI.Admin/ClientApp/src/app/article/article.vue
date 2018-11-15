<template>
  <Card>
    <span>{{ new Date() | moment("YYYY") }}</span>
      <Table :columns="[
                    {
                        title: '序号',
                        key: 'id'
                    },
                    {
                        title: '标题',
                        key: 'title'
                    },
                    {
                        title: '创建时间',
                        key: 'created',
                        render: (h,params)=>{
                            return h('div',
                                $moment(params.row.created).startOf('hour').fromNow()
                            )
                        }
                    }
                ]" :data="articles"></Table>
    </Card>
</template>

<script lang="ts" src="./article.ts"></script>